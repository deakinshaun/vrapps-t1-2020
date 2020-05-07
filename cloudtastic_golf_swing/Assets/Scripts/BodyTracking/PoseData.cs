using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseData
{
    public float[] poseFrame;
    public float deltaTime = 0;
    public long timeStamp = 0;

    public PoseData(float[] pose,long t)
    {
        deltaTime = Time.deltaTime;
        timeStamp = t;
    }
}
