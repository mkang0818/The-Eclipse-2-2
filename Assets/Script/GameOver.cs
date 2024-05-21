using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Rigidbody rigid;
    public GameObject fade;
    public GameObject text;
    public GameObject point;
    public GameObject overbutton;
    public GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Invoke("gravitiy", 6f);
         Invoke("pse", 10f);
        Invoke("textfade", 11f);
        Invoke("scorefade", 12f);
        Invoke("pointfade", 12f);
        Invoke("gameoverfade", 12f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void gravitiy()
    {
        rigid.useGravity = true;
    }

    void pse()
    {
        fade.SetActive(true);
        //Time.timeScale = 0;
    }
    void textfade()
    {
        text.SetActive(true);
    }
    void pointfade()
    {
        point.SetActive(true);
    }
    void scorefade()
    {
        score.SetActive(true);
    }
    void gameoverfade()
    {
        overbutton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
