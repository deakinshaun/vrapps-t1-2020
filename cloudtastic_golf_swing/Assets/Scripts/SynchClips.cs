using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchClips : MonoBehaviour
{

    public static SynchClips instance;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public GameObject playerPoseVisualizer;
    public GameObject expertPoseVisualizer;
    

    public void Sync()
    {
        PoseClip clip1 = playerPoseVisualizer.GetComponent<PoseSkeleton>().clip;
        PoseClip clip2 = expertPoseVisualizer.GetComponent<PoseSkeleton>().clip;
        //determine clip duration from frame timestamps.
        float clip1Duration = ClipDuration(clip1);
        float clip2Duration = ClipDuration(clip2);

        if(clip1Duration > clip2Duration)
        {
            clip2.syncFactor = clip1Duration / clip2Duration;
        }
        else
        {
            clip1.syncFactor = clip2Duration / clip1Duration;
        }

    }
    private float ClipDuration(PoseClip clip)
    {
        float clipDuration = 0;
        PoseData startFrame = clip.frames[0];
        PoseData endFrame = clip.frames[clip.frames.Count-1];
        clipDuration = endFrame.timeStamp - startFrame.timeStamp - startFrame.deltaTime * 1000;
        return clipDuration;
    }

}
