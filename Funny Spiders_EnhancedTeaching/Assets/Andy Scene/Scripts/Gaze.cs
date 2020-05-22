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

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("Tiger") || go.CompareTag("Elephant") || go.CompareTag("Monkey") || go.CompareTag("Sheep") || go.CompareTag("Whale"))
            {
                OpenInfo(go.GetComponent<InfoBehavior>());
            }
        }
        else
        {
            CloseAll();
        }
    }

    void OpenInfo(InfoBehavior desiredInfo)
    {
        foreach (InfoBehavior info in infos)
        {
            if (info == desiredInfo)
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
