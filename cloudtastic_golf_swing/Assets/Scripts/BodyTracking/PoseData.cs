using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//A serializable class for each frame
[Serializable]
public class PoseData
{       
    public long timeStamp = 0;
    public float deltaTime = 0;
    public float[] poseFrame;

    public PoseData(float[] pose,long t)
    {
        timeStamp = t;
        deltaTime = Time.deltaTime;       
        poseFrame = pose;
    }
}
