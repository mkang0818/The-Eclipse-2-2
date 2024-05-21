using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] gameObjects;

    public Transform[] spawnPosArray;
    public GameObject[] zombie;
    public int pivot;
    public float level = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new GameObject[2500]; ///오브젝트 풀링 기법
        for (int i = 0; i < 2500; i++)
        {
            GameObject gameObject = Instantiate(zombie[Random.Range(0, 6)], spawnPosArray[Random.Range(0, 6)].position, Quaternion.identity);
            gameObjects[i] = gameObject;
            gameObject.SetActive(false);
        }
        StartCoroutine(SpawnZombie());
    }
    void Update()
    {
    }
    IEnumerator SpawnZombie()
    {
        yield return new WaitForSeconds(level);
        gameObjects[pivot++].SetActive(true);
        if (pivot == 2500) pivot = 0;
        StartCoroutine(SpawnZombie());
    }
}