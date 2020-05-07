using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PoseClip
{    
    public long durationMilliseconds = 0;
    public string name;
    public List<PoseData> frames = null;

    private IEnumerator fileSaver;
    private IEnumerator fileLoader;

    public PoseClip()
    {
        frames = new List<PoseData>();
        
    }
    public void Stopped(long duration)
    {
        durationMilliseconds = duration;        
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
    
    public void SaveClip()
    {
        fileSaver = JsonSave();
        GameManager.instance.StartCoroutine(fileSaver);       
    }
    IEnumerator JsonSave()
    {
        PoseClipToSave clip;
        clip.durationMilliseconds = this.durationMilliseconds;
        clip.name = this.name;
        clip.frames = this.frames.ToArray();
        string data = JsonUtility.ToJson(clip);
        
        string path = Application.persistentDataPath + "/" + clip.name + ".json";
        Debug.Log("savePath=" + path);
        Debug.Log("dataToWrite="+data);
        try
        {
            File.WriteAllText(path, data);
            Debug.Log(data);            
        }
        catch (Exception e)
        {
            Debug.Log("unable to save file: " + path);
        }
        yield return null;       
    }

    public PoseClip LoadClip(PoseClip poseClip)
    {
        string path = Application.persistentDataPath + "/Test.json";
        try
        {
            // Open the file to read from.
            string readText = File.ReadAllText(path);
            poseClip = JsonUtility.FromJson<PoseClip>(readText);
            return poseClip;
        }
        catch (Exception e)
        {
            Debug.Log("unable to load file: " + path);
            return null;
        }
    }
    

}
