using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOver: MonoBehaviour
{
    public Animator anim;
    public int health = 10;
    NavMeshAgent agent;
    [SerializeField]
    Transform target;
    public int attack = 5;
    public float speed = 10;
    Rigidbody rigid;
    //public Gun gun;
    GameObject gun;
    CapsuleCollider capsuleCollider;
    WALL wall;

    ItemController Store;
    private void Awake()
    {
        //gun = GameObject.Find("w_rifle").GetComponent<Gun>();
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        wall = GetComponent<WALL>();
        anim = GetComponent<Animator>();
        //Store = GameObject.Find("GameManager").GetComponent<ItemController>();
        speed = agent.speed;
    }
    void Update()
    {
        agent.SetDestination(target.position);
        anim.SetBool("walk", true);
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Lwall")
        {
                InvokeRepeating("Lattackmove", 2.0f,1.0f);
                anim.SetBool("attack", true);
        }

        else if (collision.gameObject.tag == "Rwall")
        {
            InvokeRepeating("Rattackmove", 2.0f, 1.0f);
            anim.SetBool("attack", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            health -= GameObject.Find("w_rifle").GetComponent<Gun>().damage;
            Destroy(other.gameObject);
            Store.coinAmount = Store.coinAmount + 1;
        }
    }
    void Rattackmove() {
        GameObject.Find("Rtarget2").GetComponent<WALL>().wallHP -= attack;
    }
    void Lattackmove()
    {
        GameObject.Find("Ltarget2").GetComponent<WALL>().wallHP -= attack;
    }
}