using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public GameObject Spawntower;
    public GameObject Tower;

    public Transform TowerPostion;

    private StoreController coin_amount;
    // Start is called before the first frame update
    void Start()
    {
        coin_amount = GameObject.Find("GameManager").GetComponent<StoreController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpTower()
    {
        if (coin_amount.coinAmount >= 70)
        {
            coin_amount.coinAmount = coin_amount.coinAmount - 70;
            //Instantiate(Tower, TowerPostion.position, Quaternion.identity);
            GameObject.Find("Player1").GetComponent<PlayerController>().turretcount++;
        }
    }
    public void SpMoveTower()
    {
        if (coin_amount.coinAmount >= 20)
        {
            
        }
    }
}
