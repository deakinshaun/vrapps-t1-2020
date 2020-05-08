using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneSensor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion gyroAndroid = Input.gyro.attitude;
        transform.rotation = new Quaternion(gyroAndroid.x, gyroAndroid.y, -gyroAndroid.z, -gyroAndroid.w);
    }
}
