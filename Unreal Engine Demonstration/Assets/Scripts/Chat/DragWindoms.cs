using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindoms : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{


    private Vector3 offset;

    public static int siblingIndex = 0;


    public void OnPointerDown(PointerEventData data)
    {
        offset = transform.position - Input.mousePosition;

        transform.SetSiblingIndex(siblingIndex + 1);
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition + offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    
    }
}
