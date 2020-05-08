using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OverlayDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Canvas;

    private void Start()
    {
        Canvas.SetActive(false);
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Canvas.SetActive(true);
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Canvas.SetActive(false);
    }
        
       
}

