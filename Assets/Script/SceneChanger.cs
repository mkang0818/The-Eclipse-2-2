using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Text lastPoint;
    public GameObject start;
    public GameObject option;
    public GameObject exit;
    public GameObject name;
    public AudioSource bgm;
    public GameObject fadein;
    int pointstr;
    GameObject StageManager;
    void Start()
    {
        pointstr = GameObject.Find("GameManager").GetComponent<GameManager>().score;
        //start = GameObject.Find("Start");
        //option = GameObject.Find("Option");
        //exit = GameObject.Find("Exit");
        bgm.Play();
        StageManager = GameObject.Find("GameManager");
    }
    void Update()
    {
        lastPoint.text = "" + pointstr;
    }
    public void SceneGame()
    {
        Invoke("game",3.0f);
        Invoke("pse", 1.5f);
        GameObject.Find("King").GetComponent<movezom>().speed = 40.0f;
        start.SetActive(false);
        option.SetActive(false);
        exit.SetActive(false);
        name.SetActive(false);
    }
    void game()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Option()
    {
        
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("game exit");
    }

    public void Main()
    {
        SceneManager.LoadScene("MainScene");
    }

    void pse()
    {
        fadein.SetActive(true);
    }
}
