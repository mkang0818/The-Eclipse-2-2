using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public GameObject[] turret;
    public GameObject moveturret;
    [SerializeField]
    private float walkSpeed;
    public float sensitivity = 500f; //감도 설정
    public float cameraLimit = 75;
    float rotationX = 0.0f;  //x축 회전값
    float rotationY = 0.0f;  //z축 회전값


    public int turretcount = 0;
    public int maxTurretCount = 3;
    public int presentTurret = 0;

    public int moveturretcount = 0;

    public int bombcount = 0;
    public GameObject[] airplane;
    public int aircount = 0;
    public int maxAirCount = 2;
   
    

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    private Animator ani;
    private CharacterController characterCon;
    private Vector3 moveDir;
    private Transform tr;
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();    
        ani = GetComponent<Animator>();
        characterCon = GetComponent<CharacterController>();
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        Move();                 // 1️⃣ 키보드 입력에 따라 이동
        CameraRotation();       // 2️⃣ 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전

        if(GameObject.Find("HP").GetComponent<Slider>().value <= 0)
        {
            //ani.SetTrigger("Death");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {  //나가서 싸우는 병사
            if(moveturretcount > 0)
            {
                moveturretcount--;
                Instantiate(moveturret, new Vector3(this.gameObject.transform.position.x, 0, -195f), Quaternion.identity);
            }
            //moveturretcount++;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {  //포탑 최대 3개
            
            if(presentTurret == 5)
            {
                return;
            }
            else
            {
                if(turretcount > 0)
                {
                    turretcount--;
                    turret[presentTurret].SetActive(true);
                    presentTurret++;   
                }   
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (aircount > 0)
            {
                aircount--;
                Instantiate(airplane[0]);
                Instantiate(airplane[1]);
                Instantiate(airplane[2]);
            }
            /*else if (aircount == 2)
            {
                return;
            }*/
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move_H = transform.right * h;
        Vector3 move_Z = transform.forward * z;


        moveDir = (move_H + move_Z).normalized * walkSpeed;

        characterCon.Move(moveDir * Time.deltaTime);
        tr.transform.rotation = theCamera.transform.rotation;


        /*float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;
        ani.SetBool("Run", _velocity != Vector3.zero);
        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);*/
    }

    private void CameraRotation()   //상하 캐릭터 회전
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        rotationX += x * sensitivity * Time.deltaTime;
        rotationY += y * sensitivity * Time.deltaTime;

        if (rotationY > cameraLimit)
        {
            rotationY = cameraLimit;
        }
        else if (rotationY < -cameraLimit)
        {
            rotationY = -cameraLimit;
        }
        transform.eulerAngles = new Vector3(-rotationY, rotationX, 0.0f);
    }
}