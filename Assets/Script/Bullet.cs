using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor")
        {
            Destroy(gameObject);
        }
        else if(other. tag == "Buliding")
        {
            Destroy(gameObject);
        }
    }
}
