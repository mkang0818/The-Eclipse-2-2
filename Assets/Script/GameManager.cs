using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int score;
    public Slider hpSlide;
    public Text bomb;
    public Text moveTurret;
    public Text airplain;
    public Text turret;
    public int presentAirplain = 0;
    public int presentTurret = 0;
    public Text MainPoint;
    public GameObject fadein;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MainPoint.text = "P : " + score;
    }

    // Update is called once per frame
    void Update()
    {
        bomb.text = "" + GameObject.Find("Player1").GetComponent<PlayerController>().bombcount;
        moveTurret.text = "" + GameObject.Find("Player1").GetComponent<PlayerController>().moveturretcount;
        airplain.text = "" + GameObject.Find("Player1").GetComponent<PlayerController>().aircount + " / "  + GameObject.Find("Player1").GetComponent<PlayerController>().maxAirCount;
        turret.text = "" + GameObject.Find("Player1").GetComponent<PlayerController>().turretcount + " / 5";
        MainPoint.text = "/ P:" + score;
        if (hpSlide.value <= 0)
        {
            fadein.SetActive(true);
            if (fadein.GetComponent<Image>().color.a > 1.0f)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

}
