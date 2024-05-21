using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public Animator anim;
    public int health = 10;
    public bool isdead = false;
    NavMeshAgent agent;
    [SerializeField]
    Transform target;
    public float attack = 0.1f;
    public float speed = 3;
    public GameObject blood;
    public AudioClip deadsound;
    Rigidbody rigid;
    //public Gun gun;
    GameObject gun;
    CapsuleCollider capsuleCollider;
    WALL wall;
    AudioSource audioSource;

    StoreController Store;
    private void Awake()
    {
        //gun = GameObject.Find("w_rifle").GetComponent<Gun>();
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        wall = GetComponent<WALL>();
        anim = GetComponent<Animator>();
        Store = GameObject.Find("GameManager").GetComponent<StoreController>();
        speed = agent.speed;
        this.audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        agent.SetDestination(target.position);
        anim.SetBool("walk", true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Lwall")
        {
                InvokeRepeating("Lattackmove", 3.0f,3.0f);
                anim.SetBool("attack", true);
        }

        else if (collision.gameObject.tag == "Rwall")
        {
            InvokeRepeating("Rattackmove", 3.0f, 3.0f);            
            anim.SetBool("attack", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            health -= GameObject.Find("w_rifle").GetComponent<Gun>().damage;
            if (health <= 0)
            {
                if (isdead == false)
                {
                    audioSource.Play();
                    isdead = true;
                    GameObject bl = Instantiate(blood);
                    bl.transform.position = transform.position;
                    anim.SetBool("death", true);
                    Destroy(bl, 1f);
                    Destroy(gameObject, 0.5f);
                    GameObject.Find("GameManager").GetComponent<GameManager>().score += 100;
                    Debug.Log(GameObject.Find("GameManager").GetComponent<GameManager>().score);
                    Store.coinAmount += 10;
                }
            }
        }
    }
    void Death() {
        anim.SetBool("death", true);
    }
    void Rattackmove() {
        //GameObject.Find("Rtarget2").GetComponent<WALL>().wallHP -= attack;
        GameObject.Find("HP").GetComponent<Slider>().value -= attack;
    }
    void Lattackmove()
    {
        //GameObject.Find("Ltarget2").GetComponent<WALL>().wallHP -= attack;
        GameObject.Find("HP").GetComponent<Slider>().value -= attack;
    }
}