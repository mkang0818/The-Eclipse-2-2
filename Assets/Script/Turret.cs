using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject[] gameObjects;
    private int pivot = 0;
    public GameObject bulletFrefeb;
    public Transform bulletPos;
    float delay = 0.4f; //속도 조절 낮을수록 빠름
    float timer;
    private int level = 3;
    AudioSource audiosource;
    //public GameObject bulletEffect; //이펙트 추가하기
    //ParticleSystem ps;

    public Transform target;
    //터렛이 쳐다보게될 타겟
    public float range = 8f;
    //터렛이 적을 인식할 수 있는 범위
    public string enemyTag = "Enemy";
    //터렛이 인식할 적의 태그
    public Transform partToRotate;
    //터렛이 중심으로 회전할 회전축
    public float turnSpeed = 10f;
    //터렛의 회전할때 속도
    void Start()
    {
        this.audiosource = GetComponent<AudioSource>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    
    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Enemy라는 태그를 갖은 것들을 enemies[]배열에 저장
        float shortestDistance = Mathf.Infinity; //가장 짧은 거리를 무한으로 둔다.
        GameObject nearestEnemy = null; //가장 가까운 적을 null 로 둔다

        foreach (GameObject enemy in enemies) //enemy가 enemies의 수만큼 반복
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //터렛과 enemy의 거리
            if (distanceToEnemy < shortestDistance) //터렛과 enemy의 거리가 가장 짧은 거리보다 작으면 
            {
                shortestDistance = distanceToEnemy; //가장 짧은 거리는 터렛과enemy 거리가 되고
                nearestEnemy = enemy; //가장 가까운 적은 enemy가됨
            }

        }
        if (nearestEnemy != null && shortestDistance <= range) //만약 가까운 적이 없고, 가장 짧은 거리가 터렛의 범위보다 짧으면
        {
            //Debug.Log("yessssssssss");
            target = nearestEnemy.transform; //타겟은 다시한번 가장 가까운 놈으로바뀜
        }
        else
        {
            
            //Debug.Log("nonononono");
            target = null; //아니면 타겟은 없는겨
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            audiosource.Play();
            var bullet = Instantiate(bulletFrefeb, transform.position, transform.rotation).GetComponent<tbullet>();
            bullet.Fire(transform.forward);
            timer = 0;
        }
        if (target == null)
            return; //타겟없으면 카마있고

        Vector3 dir = target.position - transform.position; //타겟과 터렛의 위치를 뺀 값을 dir로 갖고
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);  //회전 하는 건데 따로설명
        //partToRotate.rotation = lookRotation;  //회전 하는 건데 따로설명
        

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //범위 만큼 선그어주기. 빨간색으로다가
        Gizmos.DrawWireSphere(transform.position, range);
    }
}