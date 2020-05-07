using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoseClip
{
    List<PoseData> frames = null;
    long durationMilliseconds = 0;
    string name;

    public PoseClip()
    {
        frames = new List<PoseData>();
        
    }
    public void Stopped(long duration)
    {
        durationMilliseconds = duration;
        Debug.Log("startstopped");
        Debug.Log("frameCount="+frames.Count + " recorded over " + durationMilliseconds + " millseconds");
        Debug.Log("endstopped");
    }
    public void TrimToEnd(int index)
    {
        frames.RemoveRange(index, frames.Count - index);
    }
    public void TrimFromstart(int index)
    {
        frames.RemoveRange(0, index);
    }
    public void addFrame(PoseData frame)
    {
        frames.Add(frame);
    }
    

}
