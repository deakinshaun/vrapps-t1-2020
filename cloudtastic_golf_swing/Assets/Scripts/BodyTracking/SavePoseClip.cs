using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct PoseClipToSave
{
    public string name;
    public long durationMilliseconds;    
    public PoseData[] frames;
};
