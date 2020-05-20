using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Gaze : MonoBehaviour
{

    List<InfoBehavior> infos = new List<InfoBehavior>();

    void Start()
    {
        infos = FindObjectsOfType<InfoBehavior>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position,transform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("Info"))
            {
                OpenInfo(go.GetComponent<InfoBehavior>());
            }
        }
        else
        {
            CloseAll();
        }
    }

    void OpenInfo(InfoBehavior desirerdInfo)
    {
        foreach (InfoBehavior info in infos)
        {
            if(info == desirerdInfo)
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
        foreach (InfoBehavior info in infos)
        {
            info.CloseInfo();
        }
    }
}
