using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10;
    public GameObject explosion;

    StoreController Store;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Store = GameObject.Find("GameManager").GetComponent<StoreController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
            Destroy(exp, 2);
            Collider[] cols = Physics.OverlapSphere(exp.transform.position, exp.transform.localScale.z * 1.5f);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].gameObject.tag == "Enemy")
                {
                    Destroy(cols[i].gameObject);
                    GameObject.Find("GameManager").GetComponent<GameManager>().score += 50;
                    Store.coinAmount += 5;
                }
            }

            Destroy(gameObject);
        }

    }
}