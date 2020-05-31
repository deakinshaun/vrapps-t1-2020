using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    
    [Tooltip("Initial speed in km/h")]
    public float initialSpeed = 80f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * (initialSpeed / 3.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
