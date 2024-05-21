using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour
{
    GameObject SplashObj;               //�ǳڿ�����Ʈ
    Text text;                           //�ǳ� �̹���

    void Awake()
    {
        SplashObj = this.gameObject;//��ũ��Ʈ ������ ������Ʈ
        text = SplashObj.GetComponent<Text>();   //�ǳڿ�����Ʈ�� �̹��� ����
    }

    void Update()
    {
        StartCoroutine("MainSplash");                        //�ڷ�ƾ    //�ǳ� ���� ����
    }



    IEnumerator MainSplash()

    {
        Color color = text.color;                          //color �� �ǳ� �̹��� ����
        for (int i = 100; i >= 0; i--)                            //for�� 100�� �ݺ� 0���� ���� �� ����
        {
            color.a += Time.deltaTime * 0.01f;               //�̹��� ���� ���� Ÿ�� ��Ÿ �� * 0.01
            text.color = color;                                //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����
        }
        yield return null;                                        //�ڷ�ƾ ����
    }
}