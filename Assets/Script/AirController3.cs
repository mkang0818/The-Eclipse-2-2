using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirController3 : MonoBehaviour
{
    private float speed = 0.3f;
    public GameObject[] Bomb;
    private int bomb = 7;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FallBomb());
        Invoke("destroy", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(0,0,speed);
    }

    IEnumerator FallBomb()
    {
        yield return new WaitForSeconds(0.2f);
        if (bomb > 0)
        {
            Instantiate(Bomb[0], new Vector3(35f, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            Instantiate(Bomb[1], new Vector3(47f, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            bomb--;
        }
        StartCoroutine(FallBomb());
    }

    void destroy()
    {
        Destroy(gameObject);
    }
}
