using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField]
    int curHp = 200;

    Rigidbody rigid;
    public Gun gun;
    //BoxCollider boxCollider;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //gun = GetComponent<Gun>();
        //boxCollider = GetComponent<BoxCollider>();
    }
    void Update()
    {
        if(curHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("¹ßµ¿");
        if (collision.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            
            curHp -= gun.damage;
            Debug.Log("Range" + curHp);
            Destroy(other.gameObject);
        }
    }
}
