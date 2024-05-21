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
        InvokeRepeating("dayplus", 1.0f, 63.0f); //53초마다 하루
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * Time.unscaledDeltaTime); //대략 1분
        //transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound *30* Time.deltaTime);
        light.color = new Color(27 / 255f, 8 / 255f, 250 / 255f, 255 / 255f); //밤 색
        if (transform.eulerAngles.x <= 250) // 아침
        {
            Destroy(GameObject.FindWithTag("Enemy"));
            //spawn.SetActive(false);
            transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * 2 * Time.unscaledDeltaTime);
            //transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecound * 30 * Time.deltaTime);
            light.color = new Color(212 / 255f, 188 / 255f, 159 / 255f, 255 / 255f); //아침 색

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
        GameObject.FindWithTag("Enemy").GetComponent<Enemy>().attack += 3;  //공격력
        GameObject.FindWithTag("Enemy").GetComponent<Enemy>().speed += 1;  //스피드
        GameObject.FindWithTag("spawn").GetComponent<SpawnManager>().level *= 0.4f;  //물량    
            Day++;
        Debug.Log(Day);
        if(Day <= 5) day.text = "DAY " + Day;
    }


}