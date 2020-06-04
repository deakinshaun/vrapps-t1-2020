using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScopeTrack : MonoBehaviour {

    private Gyroscope gyro;
    private bool gyroSupported;

    private void Start()
    {
        gyroSupported = SystemInfo.supportsGyroscope;
        if (gyroSupported)
        {
            // Get hold of the system gyroscope. 19 
            gyro = Input.gyro;
            // Switch it on. 
            gyro.enabled = true;
        }
    }
    private void Update()
    {
        if (gyroSupported)
        {
            // Map the gyro rotation into the same coordinate system as the unity camera . 
            transform.rotation = Quaternion.Euler(90, 0, 90) * gyro.attitude * Quaternion.Euler(180, 180, 0);
        }
    }
}
