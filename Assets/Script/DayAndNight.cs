using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayAndNight : MonoBehaviour
{
    public Light light;
    public TextMeshProUGUI day;
    public int Day = 1;
    public GameObject spawn;
    [SerializeField] private float secondPerRealTimeSecound;

    public bool isNight = false;

    [SerializeField] private float fogDensityCalc;

    [SerializeField] private float nightFogDensity;
    private float dayFogDensity;
    private float currentFogDensity;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        dayFogDensity = RenderSettings.fogDensity;
        InvokeRepeating("dayplus", 1.0f, 63.0f); //53�ʸ��� �Ϸ�
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * Time.unscaledDeltaTime); //�뷫 1��
        //transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound *30* Time.deltaTime);
        light.color = new Color(27 / 255f, 8 / 255f, 250 / 255f, 255 / 255f); //�� ��
        if (transform.eulerAngles.x <= 250) // ��ħ
        {
            Destroy(GameObject.FindWithTag("Enemy"));
            //spawn.SetActive(false);
            transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * 2 * Time.unscaledDeltaTime);
            //transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * 30 * Time.deltaTime);
            light.color = new Color(212 / 255f, 188 / 255f, 159 / 255f, 255 / 255f); //��ħ ��

            isNight = false;
        }
        else
            isNight = true;

        if (isNight)
        {
            if (currentFogDensity <= nightFogDensity)
                currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
            RenderSettings.fogDensity = currentFogDensity;
        }
        else
        {
            if (currentFogDensity >= dayFogDensity)
                currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
            RenderSettings.fogDensity = currentFogDensity;
        }

        if (Day > 5)
        {
            gameObject.transform.localEulerAngles = new Vector3(280, 0, 0);
        }
    }
    void dayplus()
    {
        GameObject.FindWithTag("Enemy").GetComponent<Enemy>().attack += 3;  //���ݷ�
        GameObject.FindWithTag("Enemy").GetComponent<Enemy>().speed += 1;  //���ǵ�
        GameObject.FindWithTag("spawn").GetComponent<SpawnManager>().level *= 0.4f;  //����    
            Day++;
        Debug.Log(Day);
        if(Day <= 5) day.text = "DAY " + Day;
    }


}