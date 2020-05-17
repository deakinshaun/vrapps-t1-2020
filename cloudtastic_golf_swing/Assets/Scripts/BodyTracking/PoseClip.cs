using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PoseClip
{    
    public long durationMilliseconds = 0;
    public string name;
    public float syncFactor = 1;
    public List<PoseData> frames = null;
    
    private Classification clipType;
    public enum Classification
    {
        Expert,
        Player
    }
    private IEnumerator fileSaver;
    private IEnumerator fileLoader;

    public PoseClip(Classification clipType)
    {
        frames = new List<PoseData>();
        this.clipType = clipType;
        
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
        fileSaver = JsonSave(clipType);
        GameManager.instance.StartCoroutine(fileSaver);       
    }
    IEnumerator JsonSave(Classification clipType)
    {
        PoseClipToSave clip;
        clip.durationMilliseconds = this.durationMilliseconds;
        clip.name = this.name;
        clip.frames = this.frames.ToArray();
        string data = JsonUtility.ToJson(clip);
        string path = Application.persistentDataPath;
        switch (clipType)
        {
            case Classification.Expert:
                 path += "/Expert_" + clip.name + ".json";
                break;
            case Classification.Player:
                 path += "/Player_" + clip.name + ".json";
                break;
        }
       
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

    public PoseClip LoadClip(PoseClip poseClip,Classification clipType)
    {       
        string path = Application.persistentDataPath;
        switch (clipType)
        {
            case Classification.Expert:
                path += "/Expert_Test.json";
                break;
            case Classification.Player:
                path += "/Player_Test.json";
                break;
        }
        try
        {
            // Open the file to read from.
            string readText = File.ReadAllText(path);
            poseClip = JsonUtility.FromJson<PoseClip>(readText);
            poseClip.clipType = clipType;
            poseClip.syncFactor = 1;
            return poseClip;
        }
        catch (Exception e)
        {
            Debug.Log("unable to load file: " + path);
            return null;
        }
    }
}
