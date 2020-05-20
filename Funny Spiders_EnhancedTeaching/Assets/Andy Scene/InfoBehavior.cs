using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehavior : MonoBehaviour
{
    const float SPEED = 6f;

    [SerializeField]
    Transform SectionInfo;

    Vector3 desireScale = Vector3.zero;

    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desireScale, Time.deltaTime * SPEED);
    }

    public void OpenInfo()
    {
        desireScale = Vector3.one;
    }

    public void CloseInfo()
    {
        desireScale = Vector3.zero;
    }
}
