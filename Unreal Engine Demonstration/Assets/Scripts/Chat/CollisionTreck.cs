using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

public class CollisionTreck : MonoBehaviour {

    public QuickDrag drag;


    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            print("Enter");
            drag.isStopOncollisionEnter = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            drag.isStopOncollisionEnter = false;
            print("Exit");
        }
    }
}
