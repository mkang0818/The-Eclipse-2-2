using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //�ʿ��� ������Ʈ
    [SerializeField]
    private GunController gunController;
    private Gun currentGun;

    //�Ѿ� ���� �ؽ�Ʈ �ݿ�
    [SerializeField]
    private Text[] text_Bullet;

    void Update()
    {
        CheckBullet();
    }
    
    private void CheckBullet()
    {
        currentGun = gunController.GetGun();
        text_Bullet[0].text = currentGun.carryBulletCount.ToString();
        text_Bullet[1].text = currentGun.reloadBulletCount.ToString();
        text_Bullet[2].text = currentGun.currentBulletCount.ToString();

    }
}