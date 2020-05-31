using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject scoreCanvas;
    public Text scoreText;
    public Slider scoreSlider;
    public GameObject playerPoseVisualizer;
    public GameObject expertPoseVisualizer;
    private bool scoreVisible = false;

    private void Start()
    {
        scoreVisible = false;
        scoreCanvas.SetActive(false);
    }
    //totals the distance between the skeleton joints for each frame of player and expert clips
    //divides by clip duration and updates gui with score
    public void Score()
    {
        float score = 0;
        float duration = 0;
        PoseClip playerClip = playerPoseVisualizer.GetComponent<PoseSkeleton>().clip;
        PoseClip expertClip = expertPoseVisualizer.GetComponent<PoseSkeleton>().clip;
        if(playerClip.frames.Count != expertClip.frames.Count)
        {

        }
        else
        {
            for (int i = 0; i < playerClip.frames.Count; i++)
            {
                
                PoseData playerPose = playerClip.frames[i];
                PoseData expertPose = expertClip.frames[i];
                Debug.Log("timestamp: "+playerPose.timeStamp);
                duration += playerPose.deltaTime;
                float[] playerPoints = playerPose.poseFrame;
                float[] expertPoints = expertPose.poseFrame;
                for(int x=0;x < 17;x++)
                {
                    Vector3 player = new Vector3(playerPoints[x*3+0], playerPoints[x*3+1], playerPoints[x*+2]);
                    Vector3 expert = new Vector3(expertPoints[x * 3 + 0], expertPoints[x * 3 + 1], expertPoints[x * +2]);
                    float distance = Vector3.Distance(player, expert);
                    score += distance;
                    Debug.Log(distance);
                }
                Debug.Log("");
                
            }
            Debug.Log("Distance deviation:"+score + " Duration: "+duration + "Score: " + score/duration *100f);
        }
        
        scoreCanvas.SetActive(true);
        scoreVisible = true;
        float scored = score / duration;
        scoreText.text = "Score:\n" + scored;
        if(scored > 100)
        {
            scored = 100;
        }
        if(scored < 1)
        {
            scored = 1;
        }
        scoreSlider.value = scored;
    }

    //spaces out the frame timing of the shortest motion clip to match the other clip
    public void Sync()
    {
        if(scoreVisible)
        {
            scoreCanvas.SetActive(false);
            scoreVisible = false;
            return;
        }
        if (playerPoseVisualizer != null && expertPoseVisualizer != null)
        {

            PoseClip playerClip = playerPoseVisualizer.GetComponent<PoseSkeleton>().clip;
            PoseClip expertClip = expertPoseVisualizer.GetComponent<PoseSkeleton>().clip;
            PoseClip mostFramesClip = null;
            PoseClip leastFramesClip = null;
            PoseClip synchedClip = null;

            if (playerClip != null && expertClip != null)
            {
                // Debug.Log("playerClip.syncFactor:" + playerClip.syncFactor);
                //  Debug.Log("expertClip.syncFactor:" + expertClip.syncFactor);

                if (playerClip.frames.Count > expertClip.frames.Count)
                {
                    mostFramesClip = playerClip;
                    leastFramesClip = expertClip;
                    synchedClip = new PoseClip(PoseClip.Classification.Expert);
        }
        else
        {
                    mostFramesClip = expertClip;
                    leastFramesClip = playerClip;
                    synchedClip = new PoseClip(PoseClip.Classification.Player);

                }
                try
                {
                    float lfcZeroTime = leastFramesClip.frames[0].timeStamp - leastFramesClip.frames[0].deltaTime;
                    float lfcDuration = leastFramesClip.frames[leastFramesClip.frames.Count - 1].timeStamp - lfcZeroTime;
                    float mfcZeroTime = mostFramesClip.frames[0].timeStamp - mostFramesClip.frames[0].deltaTime;
                    float mfcDuration = mostFramesClip.frames[mostFramesClip.frames.Count - 1].timeStamp - mfcZeroTime;
                    for (int i = 0; i < leastFramesClip.frames.Count; i++)
                    {
                        float portionOfClip = (leastFramesClip.frames[i].timeStamp - lfcZeroTime) / lfcDuration;
                        float syncedTime = portionOfClip * mfcDuration + mfcZeroTime;
                        leastFramesClip.frames[i].timeStamp = (long)syncedTime;
                        if (i > 0)
                        {
                            leastFramesClip.frames[i].deltaTime = (leastFramesClip.frames[i].timeStamp - leastFramesClip.frames[i - 1].timeStamp) / 1000f;
                        }
                        else
                        {
                            leastFramesClip.frames[i].deltaTime = (leastFramesClip.frames[i].timeStamp - mfcZeroTime) / 1000f;
                        }
                        Debug.Log("timeStamp:" + leastFramesClip.frames[i].timeStamp + " deltaTime:" + leastFramesClip.frames[i].deltaTime);

                    }

                }
                catch (Exception e)
                {
                    Debug.Log("error in sync");
                    Debug.Log("error: " + e.StackTrace);
                }
            }
        }
        Interpolate();
    }

    Vector3 Lerp(Vector3 start, Vector3 end, float percent)
    {//returns the point a percentage between 2 points
        return (start + percent * (end - start));
    }
    private float ClipDuration(PoseClip clip)
    {
        float clipDuration = 0;
        PoseData startFrame = clip.frames[0];
        PoseData endFrame = clip.frames[clip.frames.Count-1];
        clipDuration = endFrame.timeStamp - startFrame.timeStamp - startFrame.deltaTime * 1000;
        return clipDuration;
    }

    //Adds frames to the shorter of the 2 clips so that each has frames at same timing.
    //Uses Lerp function to interpolate skeleton points for frames with same timing. 
    //ie if we adjust the frame timing we must also interpolate the joint positions at the new timing.
    //With same number of frames at same timing we can use the score function to compare joint locations.
    public void Interpolate()
    {
        if (playerPoseVisualizer != null && expertPoseVisualizer != null)
        {

            PoseClip playerClip = playerPoseVisualizer.GetComponent<PoseSkeleton>().clip;
            PoseClip expertClip = expertPoseVisualizer.GetComponent<PoseSkeleton>().clip;
            PoseClip mostFramesClip = null;
            PoseClip leastFramesClip = null;
            PoseClip interpolatedClip = null;

            if (playerClip != null && expertClip != null)
            {               
                if (playerClip.frames.Count > 2 && expertClip.frames.Count > 2)
                {
                    if (playerClip.frames.Count > expertClip.frames.Count)
                    {
                        mostFramesClip = playerClip;
                        leastFramesClip = expertClip;
                        interpolatedClip = new PoseClip(PoseClip.Classification.Expert);
                    }
                    else
                    {
                        mostFramesClip = expertClip;
                        leastFramesClip = playerClip;
                        interpolatedClip = new PoseClip(PoseClip.Classification.Player);
                    }

                    interpolatedClip.durationMilliseconds = leastFramesClip.durationMilliseconds;                    
                    PoseData mfc_data = null;
                    PoseData interpolatedPoseData = null;
                    int iStart = 0;
                    float interpolationFactor = 1f;
                    
                  
                    for (int x = 0; x+1 < leastFramesClip.frames.Count; x++)
                    {
                        try
                        {
                            PoseData lfcA = leastFramesClip.frames[x];
                            PoseData lfcB = leastFramesClip.frames[x + 1];
                            //Debug.Log("iStart:" + iStart);
                            for (int i = iStart; i < mostFramesClip.frames.Count; i++)
                            {
                                // Debug.Log("i:" + i);
                                mfc_data = mostFramesClip.frames[i];

                                //do points of AB & mfc_data.timeStamp until timestamp of  mfc_data.timeStamp > timestamp lfcB.timestamp
                                if (mfc_data.timeStamp <= lfcB.timeStamp)
                                {
                                    iStart++;

                                    float[] extrapolatedXYZ = new float[17 * 3];
                                    interpolatedPoseData = new PoseData(extrapolatedXYZ, mfc_data.timeStamp);
                                    interpolatedPoseData.deltaTime = mfc_data.deltaTime;
                                    
                                    interpolationFactor = ((float)mfc_data.timeStamp - (float)lfcA.timeStamp) / ((float)lfcB.timeStamp - (float)lfcA.timeStamp);
                                    Debug.Log("x:" + x + " interpolationFactor:" + interpolationFactor + " mfc_data.timeStamp:" + mfc_data.timeStamp + " lfcA.timeStamp:" + lfcA.timeStamp + " lfcB.timeStamp:" + lfcB.timeStamp);
                                    //populate extrapolatedXYZ using interpolationFactor point data in lfcA and lfcB
                                    for (int p = 0; p < 17; p++)
                                    {                                       
                                        Vector3 p1 = new Vector3(lfcA.poseFrame[p * 3 + 0], lfcA.poseFrame[p * 3 + 1], lfcA.poseFrame[p * 3 + 2]);
                                        Vector3 p2 = new Vector3(lfcB.poseFrame[p * 3 + 0], lfcB.poseFrame[p * 3 + 1], lfcB.poseFrame[p * 3 + 2]);
                                       
                                        Vector3 interpolatedPoint = Lerp(p1, p2, interpolationFactor);
                                        interpolatedPoseData.poseFrame[p * 3 + 0] = interpolatedPoint.x;
                                        interpolatedPoseData.poseFrame[p * 3 + 1] = interpolatedPoint.y;
                                        interpolatedPoseData.poseFrame[p * 3 + 2] = interpolatedPoint.z;//not sure if y and z in correct order??
                                    }
                                    interpolatedClip.addFrame(interpolatedPoseData);
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }//end lfc loop
                    
                    if (leastFramesClip == playerClip)
                    {
                        playerPoseVisualizer.GetComponent<PoseSkeleton>().clip = interpolatedClip;
                    }
                    else
                    {
                        expertPoseVisualizer.GetComponent<PoseSkeleton>().clip = interpolatedClip;
                    }
                }
            }
        }
        Score();
    }
}






