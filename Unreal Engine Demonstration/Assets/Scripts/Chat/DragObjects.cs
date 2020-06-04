using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjects : MonoBehaviour {

    public static DragObjects instance;

    //public float speed = 0.1F;

    [HideInInspector]public bool isDrag = false;

    private Vector3 startPos;
    private Vector3 endPos;


    private void Awake()
    {
        instance = this;
    }
    void Update()
    {

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit,3000,8) && hit.collider.tag == "Desk")
        //    {
        //        isDrag = true; 
        //    }
        //    else
        //    {
        //        isDrag = false;
        //    }

        //}

        //if (isDrag)
        //{
        //    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        //    transform.Translate(touchDeltaPosition.x * speed, 0f, touchDeltaPosition.y * speed);
        //}


        //if (Input.touchCount > 0)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Began)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Desk")
        //        {
        //            startPos = Input.GetTouch(0).position;
        //            isDrag = true;
        //        }
        //    }
        //    else if (Input.GetTouch(0).phase == TouchPhase.Moved && isDrag)
        //    {
        //        endPos = Input.GetTouch(0).position;
        //        Vector3 movePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //        if (endPos.x >= startPos.x)
        //        {
        //            transform.position += new Vector3(movePos.x / 700, 0, 0);
        //        }
        //        else if (endPos.x < startPos.x)
        //        {
        //            transform.position += new Vector3(movePos.x / 700, 0, 0);
        //        }

        //        if (endPos.y >= startPos.y)
        //        {
        //            transform.position += new Vector3(0, 0, movePos.y / 700);
        //        }
        //        else if (endPos.y < startPos.y)
        //        {
        //            transform.position += new Vector3(0, 0, -movePos.y / 700);
        //        }
        //    }
        //    else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //    {
        //        isDrag = false;
        //    }
        //}
    }

    public void OnDragStart()
    {
        isDrag = true;
    }

    public void OnDragExit()
    {
        isDrag = false;
    }
    
}
