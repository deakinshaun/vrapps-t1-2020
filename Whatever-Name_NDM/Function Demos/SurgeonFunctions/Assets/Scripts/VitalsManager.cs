using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalsManager : MonoBehaviour
{
    public Text heartRate;
    public Text respitoryRate;
    public Text bloodPressure;
    public Text temperature;
    public Text oxygenSaturation;
    int hr;
    int rr;
    int bp;
    float temp;
    int os;
    int lowEndHR;
    int highEndHR;
    int lowEndRR;
    int highEndRR;
    int lowEndBP;
    int highEndBP;
    float lowEndTEMP;
    float highEndTEMP;
    int lowEndOS;
    int highEndOS;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("HeartRateUpdate", 0, 3);
        InvokeRepeating("RespitoryRateUpdate", 0, 3);
        InvokeRepeating("BloodPressureUpdate", 0, 3);
        InvokeRepeating("TemperatureUpdate", 0, 3);
        InvokeRepeating("OxygenSaturationUpdate", 0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HeartRateUpdate()
    {
        hr = Random.Range(lowEndHR, highEndHR);
        heartRate.text = hr.ToString();
    }
    void RespitoryRateUpdate()
    {
        rr = Random.Range(lowEndRR, highEndRR);
        respitoryRate.text = rr.ToString();
    }
    void BloodPressureUpdate()
    {
        bp = Random.Range(lowEndBP, highEndBP);
        bloodPressure.text = bp.ToString();
    }
    void TemperatureUpdate()
    {
        temp = Random.Range(lowEndTEMP, highEndTEMP);
        temperature.text = temp.ToString();
    }
    void OxygenSaturationUpdate()
    {
        os = Random.Range(lowEndOS, highEndOS);
        oxygenSaturation.text = os.ToString();
    }
    public void sixtyTrue()
    {
        lowEndHR = 60;
        highEndHR = 69;
    }
    public void seventyTrue()
    {
        lowEndHR = 70;
        highEndHR = 79;
    }
    public void eightyTrue()
    {
        lowEndHR = 80;
        highEndHR = 89;
    }
    public void ninetyTrue()
    {
        lowEndHR = 90;
        highEndHR = 99;
    }
    public void hundredTrue()
    {
        lowEndHR = 100;
        highEndHR = 109;
    }
    public void hundredtenTrue()
    {
        lowEndHR = 110;
        highEndHR = 119;
    }
    public void hundredtwenTrue()
    {
        lowEndHR = 120;
        highEndHR = 129;
    }
    public void hundredthirtyTrue()
    {
        lowEndHR = 130;
        highEndHR = 149;
    }
    public void healthyResp()
    {
        lowEndRR = 12;
        highEndRR = 20;
    }
    public void unhealthyResp()
    {
        lowEndRR = 21;
        highEndRR = 30;
    }
    public void healthyTemp()
    {
        lowEndTEMP = 37.3f;
        highEndTEMP = 37.7f;
    }
    public void lowTemp()
    {
        lowEndTEMP = 35.0f;
        highEndTEMP = 35.5f;
    }
    public void highTemp()
    {
        lowEndTEMP = 38.0f;
        highEndTEMP = 38.5f;
    }
    public void healthyOs()
    {
        lowEndOS = 95;
        highEndOS = 100;
    }
    public void unhealthyOs()
    {
        lowEndOS = 80;
        highEndOS = 90;
    }
    public void healthTrue()
    {
        lowEndHR = 70;
        highEndHR = 79;
        lowEndRR = 12;
        highEndRR = 20;
        lowEndBP = 0;
        highEndBP = 0;
        lowEndTEMP = 37.3f;
        highEndTEMP = 37.7f;
        lowEndOS = 95;
        highEndOS = 100;
    }
    public void deadTrue()
    {
        lowEndHR = 0;
        highEndHR = 0;
        lowEndRR = 0;
        highEndRR = 0;
        lowEndBP = 0;
        highEndBP = 0;
        lowEndTEMP = 0;
        highEndTEMP = 0;
        lowEndOS = 0;
        highEndOS = 0;
    }
}
