using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInButton : MonoBehaviour
{
    GameObject SplashObj;               //�ǳڿ�����Ʈ
    Text text;                           //�ǳ� �̹���
    Image image;

    void Awake()
    {
        SplashObj = this.gameObject;//��ũ��Ʈ ������ ������Ʈ
        image = SplashObj.GetComponent<Image>();
        text = SplashObj.GetComponentInChildren<Text>();    //�ǳڿ�����Ʈ�� �̹��� ����
    }

    void Update()
    {
        StartCoroutine("MainSplash");                        //�ڷ�ƾ    //�ǳ� ���� ����
    }



    IEnumerator MainSplash()

    {
        Color bucolor = image.color;
        Color color = text.color;                          //color �� �ǳ� �̹��� ����
        for (int i = 100; i >= 0; i--)                            //for�� 100�� �ݺ� 0���� ���� �� ����
        {
            bucolor.a += Time.deltaTime * 0.01f;
            color.a += Time.deltaTime * 0.01f;               //�̹��� ���� ���� Ÿ�� ��Ÿ �� * 0.01
            image.color = bucolor;
            text.color = color;                                //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����
        }
        yield return null;                                        //�ڷ�ƾ ����
    }
}