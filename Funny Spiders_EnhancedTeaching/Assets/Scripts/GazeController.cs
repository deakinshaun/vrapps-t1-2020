using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GazeController : MonoBehaviour
{
    List<InfoBehaviour> infos = new List<InfoBehaviour>();

    private void Start()
    {
        infos = FindObjectsOfType<InfoBehaviour>().ToList();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject portal = hit.collider.gameObject;

            if (portal.CompareTag("hasInfo"))
            {
                OpenInfo(portal.GetComponent<InfoBehaviour>());
            }
        }
        else
        {
            CloseAll();
        }
    }

    void OpenInfo(InfoBehaviour targetedInfo)
    {
        foreach (InfoBehaviour info in infos)
        {
            if (info == targetedInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }

    void CloseAll()
    {
        foreach (InfoBehaviour info in infos)
        {
            info.CloseInfo();
        }
    }
}
