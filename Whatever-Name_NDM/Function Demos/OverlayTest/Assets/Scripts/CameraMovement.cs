using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float move = 0.0f;
    private float turn = 0.0f;

    public float turnSpeed = 100.0f;
    public float moveSpeed = .1f;
    // Start is called before the first frame update
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(turn * turnSpeed * Time.deltaTime, Vector3.up);
        transform.position += move * moveSpeed * transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.position += v * transform.forward * Time.deltaTime * 100.0f;
        transform.rotation *= Quaternion.AngleAxis(h * 1000.0f * Time.deltaTime, transform.up);
    }
}

