using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        this.transform.rotation *= Quaternion.AngleAxis(0.5f, new Vector3(0.1f, 0.1f, 0.6f)); //Rotate object around the axis of the vector
    }
}
