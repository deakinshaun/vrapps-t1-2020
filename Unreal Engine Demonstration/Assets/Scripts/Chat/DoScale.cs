using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoScale : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler {

    public static DoScale instance;

    private Vector2 startPos;
    private Vector2 endPos;

    public GameObject windomChat;
    public bool isDrag = false;

    private void Awake()
    {
        instance = this;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = true;

        startPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;

        endPos = eventData.position;

        if (endPos.y > startPos.y)
        {
            windomChat.transform.localScale /= 1.015f;
        }
        else
        {
            windomChat.transform.localScale *= 1.015f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDrag = false;
    }
}
