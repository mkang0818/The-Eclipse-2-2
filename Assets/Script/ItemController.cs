using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public GameObject Store;
    private bool shopping = false;
    public int coinAmount = 0;
    public Text coin;
    private int firstShopping = 0;

    public Transform Button1Pos;
    public Transform Button2Pos;
    public Transform Button3Pos;

    public Transform AirPos1;
    public Transform AirPos2;
    public Transform AirPos3;

    public GameObject[] arrayObject;

    public GameObject[] SelectObject;

    public GameObject Buttons;

    bool IsPause = false;

    public GameObject[] AirPlane;
    //private bool shopping = false;
    // Start is called before the first frame update
    void Start()
    {
        Store = GameObject.Find("Store");
        Store.SetActive(false);
        coin.text = "c : " + coinAmount;
    }

    // Update is called once per frame
    void Update()
    {

        ////child[0] = GameObject.Find("Buttons").transform.GetChild(0);
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (shopping == false && firstShopping == 0 && IsPause==false)
            {
                //GameObject.Find("Player1").GetComponent<PlayerController>().enabled = false;
                //GameObject.Find("Player1").GetComponent<GunController>().enabled = false;
                //GameObject.Find("Player1").GetComponent<Gun>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                IsPause = true;
                Cursor.visible = true;
                Store.SetActive(true);              
                shopping = true;;
                firstShopping = 1;
                Reroll();
            }
            else if (shopping == false && IsPause == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Cursor.visible = true;
                IsPause = true;
                Store.SetActive(true);
                shopping = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                IsPause = false;
                Cursor.visible = false;
                Store.SetActive(false);
                shopping = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            coinAmount++;
        }

        coin.text = "c : " + coinAmount;

        if (shopping == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reroll();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AirBomb();
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
            SelectObject[i] = arrayObject[Random.Range(0, 4)];
        }

        Instantiate(SelectObject[0], Button1Pos.position, Quaternion.identity, Buttons.transform);
        Instantiate(SelectObject[1], Button2Pos.position, Quaternion.identity, Buttons.transform);
        Instantiate(SelectObject[2], Button3Pos.position, Quaternion.identity, Buttons.transform);
    }

    void AirBomb()
    {
        
        Instantiate(AirPlane[0], AirPos1.position, Quaternion.identity);
        Instantiate(AirPlane[1], AirPos2.position, Quaternion.identity);
        Instantiate(AirPlane[2], AirPos3.position, Quaternion.identity);
    }
}