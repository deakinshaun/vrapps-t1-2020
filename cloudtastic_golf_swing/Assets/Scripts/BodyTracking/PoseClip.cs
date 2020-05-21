using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class PoseClip
{    
    public long durationMilliseconds = 0;
    public string name;
    public float syncFactor = 1;
    public List<PoseData> frames = null;
    public string webPath = "http://localhost/";

    private Classification clipType;
    private string functionPHP = "functions.php";
    public enum Classification
    {
        Expert,
        Player
    }
    private IEnumerator fileSaver;
    private IEnumerator fileShare;
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
        UnityWebRequest www = new UnityWebRequest();   
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

    public void ShareClip(Classification clipType)
    {
        fileShare = JsonShare(clipType);
        GameManager.instance.StartCoroutine(fileShare);
    }
    IEnumerator JsonShare(Classification clipType)
    {
        PoseClipToSave clip;
        clip.durationMilliseconds = this.durationMilliseconds;
        clip.name = this.name;
        clip.frames = this.frames.ToArray();
        string data = JsonUtility.ToJson(clip);
        switch (clipType)
        {
            case Classification.Expert:
                break;
            case Classification.Player:
                break;
        }

        Debug.Log("dataToWrite=" + data);
        try
        {
        }
        catch (Exception e)
        {
        }
        yield return null;
    }


}
