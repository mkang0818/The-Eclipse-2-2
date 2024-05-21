using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreController : MonoBehaviour
{
    public GameObject Store;
    private bool shopping = false;
    public int coinAmount = 0;
    public Text coin;
    int buyTurret = 0;
    public Text MainCoin;
    public int towerCoin = 300;
    public TextMeshProUGUI towerText;
    public Transform Button1Pos;
    public Transform Button2Pos;
    public Transform Button3Pos;

    public GameObject[] arrayObject;

    public GameObject[] SelectObject;

    public GameObject Buttons;

    public GameObject lineCol;
    public GameObject lineRow;
    public AudioClip buySound;
    private AudioSource audioSource;
    bool IsPause = false;

    public PlayerController playerController;
    //private bool shopping = false;
    // Start is called before the first frame update
    void Start()
    {
        Store = GameObject.Find("Store");
        Store.SetActive(false);
        coin.text = "" + coinAmount;
        MainCoin.text = "" + coinAmount;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        towerText.text = "TOWER : " + towerCoin + "c";
        //child[0] = GameObject.Find("Buttons").transform.GetChild(0);
        if (!GameObject.FindWithTag("Light").GetComponent<DayAndNight>().isNight)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            IsPause = true;
            Cursor.visible = true;
            Store.SetActive(true);
            lineCol.SetActive(false);
            lineRow.SetActive(false);
            shopping = true; ;
            //Reroll();
        }
        if (GameObject.FindWithTag("Light").GetComponent<DayAndNight>().isNight)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            IsPause = false;
            lineCol.SetActive(true);
            lineRow.SetActive(true);
            Cursor.visible = false;
            Store.SetActive(false);
            shopping = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            coinAmount += 1000;
        }

        coin.text = "" + coinAmount;
        MainCoin.text = "" + coinAmount;

        if (shopping == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reroll();
            }
        }
    }


    void Reroll()
    {
        for (int j = 0; j < Buttons.transform.childCount; j++)
        {
            Destroy(Buttons.transform.GetChild(j).gameObject);
        }
        for (int i = 0; i < 3; i++)
        {
            SelectObject[i] = arrayObject[Random.Range(0, 6)];
        }

        Instantiate(SelectObject[0], Button1Pos.position, Quaternion.identity, Buttons.transform);
        Instantiate(SelectObject[1], Button2Pos.position, Quaternion.identity, Buttons.transform);
        Instantiate(SelectObject[2], Button3Pos.position, Quaternion.identity, Buttons.transform);
    }

    public void SpTower()
    {
        if (buyTurret == 5)
        {
            return;
        }
        else if (playerController.maxTurretCount > playerController.turretcount)
        {
            if (coinAmount >= towerCoin)
            {
                buyTurret++;
                coinAmount -= towerCoin;
                towerCoin += 30;
                playerController.turretcount++;
                PlaySE(buySound);
                Reroll();
            }
        }

    }

    public void SpMoveTower()
    {
        if (coinAmount >= 100)
        {
            coinAmount -= 100;
            playerController.moveturretcount++;
            PlaySE(buySound);
            Reroll();
        }

    }

    public void SpBomb()
    {
        if (coinAmount >= 50)
        {
            coinAmount -= 50;
            playerController.bombcount++;
            PlaySE(buySound);
            Reroll();
        }
    }

    public void SpAirplain()
    {
        if (playerController.maxAirCount > playerController.aircount)
        {
            if (coinAmount >= 600)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().presentAirplain++;
                coinAmount -= 600;
                playerController.aircount++;
                PlaySE(buySound);
                Reroll();
            }
        }

    }
    public void Heel()
    {
        if (coinAmount >= 100)
        {
            coinAmount -= 100;
            GameObject.Find("HP").GetComponent<Slider>().value += 100;
            PlaySE(buySound);
            Reroll();

        }
    }

    public void ReloadUp()
    {
        if (coinAmount >= 30)
        {
            coinAmount -= 30;
            GameObject.Find("Player1").GetComponent<GunController>().currentGun.reloadBulletCount += 15;
            GameObject.Find("Player1").GetComponent<GunController>().currentGun.fireRate *= 0.95f;
            PlaySE(buySound);
            Reroll();
        }
    }

    public void Bullet()
    {
        if (coinAmount >= 50)
        {
            coinAmount -= 50;
            GameObject.Find("Player1").GetComponent<GunController>().currentGun.carryBulletCount += 100;
            PlaySE(buySound);
        }
    }
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}