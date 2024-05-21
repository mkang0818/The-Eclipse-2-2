using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //현재 장착된 총
    public Gun currentGun;
    
    //연사속도
    private float currentFireRate;

    
    private bool isReload = false;

    public GameObject BombFactory;
    public Transform bulletPos;
    public GameObject bullet;

    private Animator ani;
    private AudioSource audioSource;
    //private RaycastHit hit;

    [SerializeField]
    private Camera theCam;

    void Start()
    {
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        BombShoot();
    }
    //연사속도 재계산
    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }
    //발사 시도
    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
    }

    //발사 전 계산
    private void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBulletCount > 0)
                Shoot();
            else
                StartCoroutine(ReloadCoroutine());
        }
        
    }
    //발사 후 계산
    private void Shoot()
    {
        ani.SetTrigger("Shoot");
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        PlaySE(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        //var flash = Instantiate(currentGun.muzzleFlash, currentGun.muzzlePos.transform);
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * currentGun.bulletSpeed;
        //Hit();
        //Debug.Log("발사함");
    }

    /*private void Hit()
    {
        if (Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hit, currentGun.range))
        {
            if()
        }
    }*/


    //재장전시도
    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }
    
    //재장전
    IEnumerator ReloadCoroutine()
    {
        if(currentGun.carryBulletCount > 0)
        {
            isReload = true;
            ani.SetTrigger("Shoot");
            PlaySE(currentGun.reload_Sound);
            yield return new WaitForSeconds(currentGun.reloadTime);

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0; 

            if(currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else
            {
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;
            }
            isReload = false;

        }
        else
        {
            Debug.Log("소유한 총알이 없습니다.");
        }
    }
    private void PlaySE(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
    public Gun GetGun()
    {
        return currentGun;
    }

    private void BombShoot()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(GameObject.Find("Player1").GetComponent<PlayerController>().bombcount > 0)
            {
                GameObject.Find("Player1").GetComponent<PlayerController>().bombcount--;
                GameObject bomb = Instantiate(BombFactory);
                bomb.transform.position = theCam.transform.position;
                bomb.transform.forward = theCam.transform.forward;
            }
            else
            {

            }
            
        }
    }
}
