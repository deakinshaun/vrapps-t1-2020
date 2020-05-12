using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 * A struct to contain motion capture data for saving out to file. 
 * Has only the serializable content and none of the methods etc associated with a motion capture clip.
 * 
 */

[Serializable]
public struct PoseClipToSave
{
    public string name;
    public long durationMilliseconds;    
    public PoseData[] frames;
};
