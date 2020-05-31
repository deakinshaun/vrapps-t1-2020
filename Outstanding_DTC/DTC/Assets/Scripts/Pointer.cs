using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pointer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public bool isTeleporter;
    public bool isTool;
    public GameObject teleportPosition;
    private GameObject spawnedTeleportPosition;
    private GameObject selectedGameObject;
    public RecordController recordController;
    public TextMeshProUGUI text;
    public Transform avatar;
    private Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        selectedGameObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        // update the pointer line renderers
        // 1m extension by default unless something has been detected
        // currently set to infinity
        lineRenderer.SetPosition(0, transform.position);
        if (isTeleporter)
        {
            lineRenderer.SetPosition(1, transform.position);
        }

        if (selectedGameObject)
        {
            lineRenderer.SetPosition(1, transform.position);
            text.text = selectedGameObject.transform.position.ToString();
            text.text += '\n' + selectedGameObject.transform.localEulerAngles.ToString() + ' ' + selectedGameObject.GetComponent<RecordableObject>().speed;

            if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick, OVRInput.Controller.RTouch))
            {
                selectedGameObject.transform.eulerAngles = Vector3.zero;
            }
                
            float speed = selectedGameObject.transform.gameObject.GetComponent<RecordableObject>().speed;
            speed += OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y * Time.unscaledDeltaTime;
            selectedGameObject.transform.gameObject.GetComponent<RecordableObject>().speed = speed;
            
            RaycastHit groundHit;
            if (Physics.Raycast(transform.position, transform.forward, out groundHit, Mathf.Infinity, 1 << 8) && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.2f)
            {
                selectedGameObject.transform.position = groundHit.point + selectedGameObject.transform.up * selectedGameObject.transform.position.y;
                selectedGameObject.transform.eulerAngles += Vector3.up * OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x * Time.unscaledDeltaTime * 20f;

            }

            if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) < 0.2f)
            {
                selectedGameObject = null;
                text.text = "";
            }

            return;
        }
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (isTool)
            {
                lineRenderer.SetPosition(1, hit.point);
                // here we can do some stuff with the tool
                // if we are recording or playing back, we don't want the pointer here to do anything
                if (recordController.isRecording || recordController.isPlayback)
                {
                    return;
                }
                
                if (hit.transform.root.GetComponent<RecordableObject>())
                {
                    if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.2f)
                    {
                        selectedGameObject = hit.transform.root.gameObject;
                    }
                }
            }

            if (isTeleporter)
            {
                if (avatar.parent == null)
                {
                    if (OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).magnitude > 0)
                    {
                        lineRenderer.SetPosition(0, transform.position + transform.forward * 0.1f);
                        lineRenderer.SetPosition(1, hit.point);
                        if (spawnedTeleportPosition)
                        {
                            spawnedTeleportPosition.transform.position = hit.point + Vector3.up * 0.01f;
                        }
                        else
                        {
                            spawnedTeleportPosition = Instantiate(teleportPosition, hit.point + Vector3.up * 0.01f, Quaternion.identity);
                        }
                    }
                    else
                    {
                        if (spawnedTeleportPosition)
                        {
                            if (hit.transform.root.GetComponent<RecordableObject>())
                            {
                                respawnPosition = avatar.position;
                                avatar.SetParent(hit.transform);
                                avatar.localPosition = Vector3.zero - avatar.GetComponent<OVRCameraRig>().centerEyeAnchor.localPosition;
                            }
                            else
                            {
                                transform.root.position = spawnedTeleportPosition.transform.position;
                            }
                            
                            Destroy(spawnedTeleportPosition);
                        }
                    }
                }
                else
                {
                    if (OVRInput.GetDown(OVRInput.Button.Four))
                    {
                        avatar.SetParent(null);
                        avatar.position = respawnPosition;
                    }
                }
            }
        }
        else
        {
            if (isTool)
            {
                lineRenderer.SetPosition(1, transform.position + transform.forward * 1.0f);
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position);
            }
        }
    }
}