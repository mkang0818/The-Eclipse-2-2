using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject[] gameObjects;
    private int pivot = 0;
    public GameObject bulletFrefeb;
    public Transform bulletPos;
    float delay = 0.4f; //�ӵ� ���� �������� ����
    float timer;
    private int level = 3;
    AudioSource audiosource;
    //public GameObject bulletEffect; //����Ʈ �߰��ϱ�
    //ParticleSystem ps;

    public Transform target;
    //�ͷ��� �Ĵٺ��Ե� Ÿ��
    public float range = 8f;
    //�ͷ��� ���� �ν��� �� �ִ� ����
    public string enemyTag = "Enemy";
    //�ͷ��� �ν��� ���� �±�
    public Transform partToRotate;
    //�ͷ��� �߽����� ȸ���� ȸ����
    public float turnSpeed = 10f;
    //�ͷ��� ȸ���Ҷ� �ӵ�
    void Start()
    {
        this.audiosource = GetComponent<AudioSource>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    
    void UpdateTarget()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Enemy��� �±׸� ���� �͵��� enemies[]�迭�� ����
        float shortestDistance = Mathf.Infinity; //���� ª�� �Ÿ��� �������� �д�.
        GameObject nearestEnemy = null; //���� ����� ���� null �� �д�

        foreach (GameObject enemy in enemies) //enemy�� enemies�� ����ŭ �ݺ�
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //�ͷ��� enemy�� �Ÿ�
            if (distanceToEnemy < shortestDistance) //�ͷ��� enemy�� �Ÿ��� ���� ª�� �Ÿ����� ������ 
            {
                shortestDistance = distanceToEnemy; //���� ª�� �Ÿ��� �ͷ���enemy �Ÿ��� �ǰ�
                nearestEnemy = enemy; //���� ����� ���� enemy����
            }

        }
        if (nearestEnemy != null && shortestDistance <= range) //���� ����� ���� ����, ���� ª�� �Ÿ��� �ͷ��� �������� ª����
        {
            //Debug.Log("yessssssssss");
            target = nearestEnemy.transform; //Ÿ���� �ٽ��ѹ� ���� ����� �����ιٲ�
        }
        else
        {
            
            //Debug.Log("nonononono");
            target = null; //�ƴϸ� Ÿ���� ���°�
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
            return; //Ÿ�پ����� ī���ְ�

        Vector3 dir = target.position - transform.position; //Ÿ�ٰ� �ͷ��� ��ġ�� �� ���� dir�� ����
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);  //ȸ�� �ϴ� �ǵ� ���μ���
        //partToRotate.rotation = lookRotation;  //ȸ�� �ϴ� �ǵ� ���μ���
        

    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //���� ��ŭ ���׾��ֱ�. ���������δٰ�
        Gizmos.DrawWireSphere(transform.position, range);
    }
}