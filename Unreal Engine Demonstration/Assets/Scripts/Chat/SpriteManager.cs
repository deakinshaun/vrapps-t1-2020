using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

    public Camera main;

    private void FixedUpdate()
    {
        transform.LookAt(main.transform);
    }
}
