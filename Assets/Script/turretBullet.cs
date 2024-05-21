using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBullet : MonoBehaviour
{
    bool isFire;
    Vector3 direction;
    public float speed = 10f;
    public GameObject turret;
    public Rigidbody rigid;

    public void Fire(Vector3 dir)
    {
        direction = dir;
        isFire = true;
        Destroy(gameObject, 10f);
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            //rigid.velocity = gameObject.;
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

}
