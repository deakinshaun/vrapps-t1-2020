using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Transform camera;
    Vector3 targetAngle = Vector3.zero;

    void Start()
    {
        camera = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(camera);
        targetAngle = transform.localEulerAngles;
        targetAngle.x = 0;
        targetAngle.z = 0;
        transform.localEulerAngles = targetAngle;
    }
}
