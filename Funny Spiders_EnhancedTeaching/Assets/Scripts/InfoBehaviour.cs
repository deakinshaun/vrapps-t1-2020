using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehaviour : MonoBehaviour
{
    private const float SPEED = 6f;
    [SerializeField]
    Transform PortalInfo;

    Vector3 targetedScale = Vector3.zero;

    private void Update()
    {
        PortalInfo.localScale = Vector3.Lerp(PortalInfo.localScale, targetedScale, Time.deltaTime * SPEED);
    }

    public void OpenInfo()
    {
        targetedScale = Vector3.one;
    }

    public void CloseInfo()
    {
        targetedScale = Vector3.zero;
    }
}
