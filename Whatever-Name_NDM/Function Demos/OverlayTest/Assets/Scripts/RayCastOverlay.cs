using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCastOverlay : MonoBehaviour
{
    private bool Overlay;
    public Camera camera;
    GameObject temporaryObject;
    
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.tag == "Monitor")
                {
                    Overlay = hit.collider.gameObject.GetComponent<OverlayManager>().OverlayShown = true;
                   temporaryObject = hit.collider.gameObject;
                }
                else if (temporaryObject != null)
                {
                    temporaryObject.GetComponent<OverlayManager>().OverlayShown = false;
            }

            }
            
        }
    }
}
