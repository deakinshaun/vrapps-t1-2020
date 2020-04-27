using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;

public class TrackingFound : MonoBehaviour , ITrackableEventHandler
{
    // Start is called before the first frame update
    public GameObject spawnerObject;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||  newStatus == TrackableBehaviour.Status.TRACKED)
        {
       
        }
        else
        {
        }
    }
}
