using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

//A container for posedata related to a single motion data capture. (clip)
public class PoseClip
{    
    public long durationMilliseconds = 0;
    public string name;
    public float syncFactor = 1;   
    public List<PoseData> frames = null;    
    public Classification clipType;
    public enum Classification
    {
        Expert,
        Player
    }
    private IEnumerator fileSaver;
    private IEnumerator fileLoader;
    private IEnumerator fileShare;

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
        this.clipType = clipType;
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
    public PoseClip LoadClip(PoseClip poseClip, Classification clipType)
    {
        poseClip.clipType = clipType;
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
    #region ByGeoff
    //By Geoff Newman SID 215291967
    public PoseClip LoadSharedClip(PoseClip poseClip, Classification clipType)
    {
        poseClip.clipType = clipType;

        try
        {
            // Open the file to read from.
            string readText = GameManager.instance.swingData;
            poseClip = JsonUtility.FromJson<PoseClip>(readText);
            poseClip.clipType = clipType;
            poseClip.syncFactor = 1;
            return poseClip;
        }
        catch (Exception e)
        {
            Debug.Log("unable to load shared data: ");
            return null;
        }
    }
    public void ShareClip()
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


        string address = GameManager.instance.webHost + GameManager.instance.webFunctions;
        using (UnityWebRequest www = UnityWebRequest.Get(address+ "?DATA=" + data))
        {
            yield return www.SendWebRequest();
            Debug.Log(www.url);
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string url = (GameManager.instance.webHost + "?VID=" + www.downloadHandler.text);
                Application.OpenURL(string.Format("sms:?body=" + url));
            }
        }
    }
    #endregion
}
