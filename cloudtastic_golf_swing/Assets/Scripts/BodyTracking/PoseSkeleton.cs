using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//contains the functions to display motion data, and initiate most of the gui actions
public class PoseSkeleton : MonoBehaviour
{
    enum BodyPart : int
    {
        Nose = 0,
        Left_eye = 1,
        Right_eye = 2,
        Left_ear = 3,
        Right_ear = 4,
        Left_shoulder = 5,
        Right_shoulder = 6,
        Left_elbow = 7,
        Right_elbow = 8,
        Left_wrist = 9,
        Right_wrist = 10,
        Left_hip = 11,
        Right_hip = 12,
        Left_knee = 13,
        Right_knee = 14,
        Left_ankle = 15,
        Right_ankle = 16
    };

    [Tooltip("A marker prefab to represent pose joints in the skeleton")]
    public GameObject pointMarkerTemplate;

    [Tooltip("A prefab to represent a bone. Size and shape matching default cylinder")]
    public GameObject boneTemplate;
    public Text info;
    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject stopButton;
    public GameObject recButton;
    public Slider frameSlider;

    private GameObject[] markers;

    private int numPointsInPose;

    public PoseClip clip = null;
    public PoseClip compareClip = null;
    private int frame = 0;
    private int clipCounter = 0;
    private float poseMultiplier = 0;
    private bool playingClip = false;
    private bool paused = false;
    private float playBackSpeed = 1;
    private IEnumerator clipPlayer;
    private PoseClip.Classification clipType;

    class SkeletonBone
    {
        public GameObject boneObject;
        public BodyPart from;
        public BodyPart to;

        public SkeletonBone(BodyPart f, BodyPart t)
        {
            boneObject = null;
            from = f;
            to = t;
        }
    };

    private List<SkeletonBone> bones;

    public PoseSkeleton()
    {
        numPointsInPose = Enum.GetNames(typeof(BodyPart)).Length;
        markers = new GameObject[numPointsInPose];

        bones = new List<SkeletonBone>();

        // head
        bones.Add(new SkeletonBone(BodyPart.Left_eye, BodyPart.Right_eye));
        bones.Add(new SkeletonBone(BodyPart.Left_eye, BodyPart.Left_shoulder));
        bones.Add(new SkeletonBone(BodyPart.Right_eye, BodyPart.Right_shoulder));
        bones.Add(new SkeletonBone(BodyPart.Left_eye, BodyPart.Left_ear));
        bones.Add(new SkeletonBone(BodyPart.Right_eye, BodyPart.Right_ear));
        bones.Add(new SkeletonBone(BodyPart.Left_eye, BodyPart.Nose));
        bones.Add(new SkeletonBone(BodyPart.Right_eye, BodyPart.Nose));

        // body
        bones.Add(new SkeletonBone(BodyPart.Left_hip, BodyPart.Left_shoulder));
        bones.Add(new SkeletonBone(BodyPart.Right_hip, BodyPart.Right_shoulder));
        bones.Add(new SkeletonBone(BodyPart.Left_hip, BodyPart.Right_hip));
        bones.Add(new SkeletonBone(BodyPart.Left_shoulder, BodyPart.Right_shoulder));

        // arms
        bones.Add(new SkeletonBone(BodyPart.Left_shoulder, BodyPart.Left_elbow));
        bones.Add(new SkeletonBone(BodyPart.Left_elbow, BodyPart.Left_wrist));
        bones.Add(new SkeletonBone(BodyPart.Right_shoulder, BodyPart.Right_elbow));
        bones.Add(new SkeletonBone(BodyPart.Right_elbow, BodyPart.Right_wrist));

        // legs
        bones.Add(new SkeletonBone(BodyPart.Left_hip, BodyPart.Left_knee));
        bones.Add(new SkeletonBone(BodyPart.Left_knee, BodyPart.Left_ankle));
        bones.Add(new SkeletonBone(BodyPart.Right_hip, BodyPart.Right_knee));
        bones.Add(new SkeletonBone(BodyPart.Right_knee, BodyPart.Right_ankle));

    }

    private Vector3 poseToVector(float[] rawPoseData, int i, float poseMultiplier)
    {
        if (rawPoseData.Length > 0)
        {
            return new Vector3(-(10.0f * rawPoseData[i * 3 + 0] - 5.0f), 0.0f, -(poseMultiplier * rawPoseData[i * 3 + 1] - 5.0f));
        }
        else
        {
            return new Vector3(-1, -1, -1);
        }
    }

    private void drawPosePoints(float[] rawPoseData)
    {
        for (int i = 0; i < numPointsInPose; i++)
        {
            if (markers[i] == null)
            {
                markers[i] = Instantiate(pointMarkerTemplate);
                markers[i].transform.SetParent(this.transform, false);
                markers[i].transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            if (rawPoseData.Length > 0 && rawPoseData[i * 3 + 2] < 0.0f)
            {

                markers[i].SetActive(false);
            }
            else
            {
                markers[i].transform.localPosition = poseToVector(rawPoseData, i, poseMultiplier);
                markers[i].SetActive(true);
            }
        }
    }

    private void drawSkeleton(float[] rawPoseData)
    {
        foreach (SkeletonBone b in bones)
        {
            Vector3 from = poseToVector(rawPoseData, (int)b.from, poseMultiplier);
            Vector3 to = poseToVector(rawPoseData, (int)b.to, poseMultiplier);
            float len = 0.5f * (to - from).magnitude;

            if (len > 0.01f)
            {
                if (b.boneObject == null)
                {
                    b.boneObject = Instantiate(boneTemplate);
                    b.boneObject.transform.SetParent(this.transform, false);
                }

                b.boneObject.transform.up = transform.worldToLocalMatrix.MultiplyVector(to - from);
                b.boneObject.transform.localPosition = (from + to) / 2.0f;
                b.boneObject.transform.localScale = new Vector3(0.05f, 0.9f * len, 0.05f);

                if ((rawPoseData[(int)b.from * 3 + 2] < 0.0f) || (rawPoseData[(int)b.to * 3 + 2] < 0.0f))
                {
                    b.boneObject.SetActive(false);
                }
                else
                {
                    b.boneObject.SetActive(true);
                }
            }
            else
            {
                if (b.boneObject != null)
                {
                    b.boneObject.SetActive(false);
                }
            }
        }
    }
    public void Start()
    {
        clipPlayer = PlayCoroutine();
        stopButton.SetActive(false);
        pauseButton.SetActive(false);
        if (clip != null)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void stopPlay()
    {
        clipCounter = 0;
        playingClip = false;
        StopCoroutine(clipPlayer);
        float[] empty = { };
        drawPosePoints(empty);
        drawSkeleton(empty);
        stopButton.SetActive(false);
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        recButton.SetActive(true);
        CameraManager.instance.initCamera();
    }

    public void PlayClip()
    {
        if (clip != null && clip.frames.Count > 0)
        {
            paused = false;
            playingClip = true;

            UIManager.instance.rawImage.GetComponent<RawImage>().texture = UIManager.instance.background;
            poseMultiplier = UIManager.instance.rawImage.GetComponent<RawImage>().mainTexture.height / 40f;

            stopButton.SetActive(true);
            pauseButton.SetActive(true);
            playButton.SetActive(false);
            recButton.SetActive(false);
            CameraManager.instance.stopCamera();
            StartCoroutine(clipPlayer);
        }
    }
    public void loadPlayerClip()
    {
        loadClip(PoseClip.Classification.Player);
        if (clip != null)
        {
            this.clipType = PoseClip.Classification.Player;
        }
    }
    public void LoadExpertClip()
    {
        loadClip(PoseClip.Classification.Expert);
        if (clip != null)
        {
            this.clipType = PoseClip.Classification.Expert;
        }
    }
    public void loadClip(PoseClip.Classification clipType)
    {
        if (clip == null)
        {
            clip = new PoseClip(clipType);
        }
        clip = clip.LoadClip(clip, clipType);
        if (clip != null)
        {
            clipCounter = 0;
            UIManager.instance.rawImage.GetComponent<RawImage>().texture = UIManager.instance.background;
            playButton.SetActive(true);
        }
    }
    #region ByGeoff
    //By Geoff Newman SID 215291967
    public void loadSharedClip(PoseClip.Classification clipType = PoseClip.Classification.Player)
    {
        if (clip == null)
        {
            clip = new PoseClip(clipType);
        }
        clip = clip.LoadSharedClip(clip, clipType);
        if (clip != null)
        {
            clipCounter = 0;
            UIManager.instance.rawImage.GetComponent<RawImage>().texture = UIManager.instance.background;
            playButton.SetActive(true);
        }
    }


    public void shareCurrentClip()
    {
        if (clip != null)
        {
            clip.name = "Test";
            clip.ShareClip();
        }
    }
    #endregion
    public void saveCurrentClip()
    {
        if (clip != null)
        {
            clip.name = "Test";
            clip.SaveClip();
        }
    }

    public void TrimToEnd()
    {
        paused = true;
        clip.frames.RemoveRange(frame, clip.frames.Count - frame);
        clipCounter = clip.frames.Count - 1;
        frame = clipCounter;
        frameSlider.SetValueWithoutNotify((float)clipCounter / clip.frames.Count * 100f);
        //paused = false;
    }
    public void TrimFromStart()
    {
        clip.frames.RemoveRange(0, frame);
    }
    IEnumerator PlayCoroutine()
    {
        while (playingClip)
        {
            if (clip != null && clip.frames.Count > 0)
            {
                if (!paused)
                {
                    drawPosePoints(clip.frames[clipCounter].poseFrame);
                    drawSkeleton(clip.frames[clipCounter].poseFrame);
                    info.text = ": " + frame;
                    frame = clipCounter;
                    clipCounter++;

                    frameSlider.SetValueWithoutNotify((float)clipCounter / clip.frames.Count * 100f);

                    if (clipCounter >= clip.frames.Count)
                    {
                        clipCounter = 0;
                    }
                }
                else
                {
                    clipCounter = frame;
                    info.text = ": " + frame;
                    drawPosePoints(clip.frames[clipCounter].poseFrame);
                    drawSkeleton(clip.frames[clipCounter].poseFrame);
                }

                yield return new WaitForSeconds(clip.frames[clipCounter].deltaTime * clip.syncFactor * playBackSpeed);
                //yield return new WaitForSecondsRealtime(clip.frames[clipCounter].deltaTime * playBackSpeed);
            }
        }

    }
    public void PlaybackFrame(float percentage)
    {
        if (clip != null)
        {
            paused = true;
            frame = (int)((percentage / 100) * clip.frames.Count);
            if (frame < 0)
            {
                frame = 0;
            }
            else if (frame >= clip.frames.Count)
            {
                frame = clip.frames.Count - 1;
            }
            info.text = ": " + frame;
        }
    }
    public void PlayBackSpeed(float val)
    {
        playBackSpeed = val;
    }

    public void pauseClip()
    {
        if (paused)
        {
            paused = false;
        }
        else
        {
            paused = true;

        }
    }

    public void updatePose(float[] rawPoseData)
    {
        poseMultiplier = UIManager.instance.rawImage.GetComponent<RawImage>().mainTexture.height / 40f;
        drawPosePoints(rawPoseData);
        drawSkeleton(rawPoseData);
    }
}
