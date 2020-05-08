using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FaceCamera : MonoBehaviour
{
    Transform camera;
    Vector3 targetAngle = Vector3.zero;

    private void Start()
    {
        camera = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(camera);
        targetAngle = transform.localEulerAngles;
        targetAngle.x = 0;
        targetAngle.z = 0;
        transform.localEulerAngles = targetAngle;
    }
}
