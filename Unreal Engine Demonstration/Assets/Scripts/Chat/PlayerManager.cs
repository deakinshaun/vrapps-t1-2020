using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using HedgehogTeam.EasyTouch;


public enum PlayerState
{
    walk,
    idle
}
public class PlayerManager : NetworkBehaviour
{

    public Animation anim;
    //public float speed = 5.0f;

    public AnimationClip walk;
    public AnimationClip idle;

    private PlayerState state = PlayerState.idle;
    //private Vector3 target=Vector3.zero;

    private Vector3 fristPos;//接触时的position
    private Vector3 movePos;//移动后的position
    public float speet = 1.0f;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }
    private void FixedUpdate()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.forward * v * Time.deltaTime * speed);
        //transform.Rotate(Vector3.up * Time.deltaTime * 90.0f*h);



        //if (h != 0||v!=0)
        //{
        //    anim.clip = walk;
        //    anim.Play();
        //}
        //else
        //{
        //    anim.clip = idle;
        //    anim.Play();
        //}


        //MobileInput();
        //TouchTapMovePlayer();

        if (DoScale.instance.isDrag || DragObjects.instance.isDrag) return;

        Gesture currentGestrue = EasyTouch.current;

        if (currentGestrue != null && currentGestrue.swipe == EasyTouch.SwipeDirection.Up)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speet);
            state = PlayerState.walk;
        }
        if (currentGestrue != null && currentGestrue.swipe == EasyTouch.SwipeDirection.Down)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -speet);
            state = PlayerState.walk;
        }
        if (currentGestrue != null && currentGestrue.swipe == EasyTouch.SwipeDirection.Right)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 90.0f);
            state = PlayerState.walk;
        }
        if (currentGestrue != null && currentGestrue.swipe == EasyTouch.SwipeDirection.Left)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -90.0f);
            state = PlayerState.walk;
        }

        //if (currentGestrue != null && DragObjects.instance.isDrag != true)
        //{
        //    state = PlayerState.walk;
        //}
        if (currentGestrue == null || DragObjects.instance.isDrag == true)
        {
            state = PlayerState.idle;
        }

        if (state == PlayerState.walk)
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            anim.clip = walk;
            anim.Play();
        }
        else if (state == PlayerState.idle)
        {
            anim.clip = idle;
            anim.Play();
        }
    }

    //private void TouchTapMovePlayer()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit)&&hit.collider.tag=="Floor")
    //        {
    //            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
    //            {
    //                target = (transform.position - hit.point);
    //                target.y = transform.position.y;
    //                transform.LookAt(target);
    //                state = PlayerState.walk;
    //            }
    //        }
    //    }

    //    if (Vector3.Distance(transform.position, target) <= 0.5f)
    //    {
    //        state = PlayerState.idle;
    //    }
    //}


    private void MobileInput()//手机触屏移动的方法
    {

        float moveY = 0;// 上下移动的数值;        
        float moveX = 0;//左右移动的数值;
                        //是否刚刚触屏

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //接触屏幕的坐标
            fristPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)//是否触屏移动
        {                                  //触屏移动后的坐标      
            movePos = Input.GetTouch(0).deltaPosition;

            //更据X轴的值判断移动方向
            if (Mathf.Abs(fristPos.x - movePos.x) > 0.1)
            {
                //向右移动
                if (fristPos.x < movePos.x)
                {
                    /*判断移动是否会超出屏幕右侧
                    700这个值是针对不同手机屏幕的分辩率参考出来的,因为分辨率越高，手指在屏幕中移动的值不一样。
                    比如：400*800  与 1920*1080的分辨率,手指在屏幕移动1厘米的距离得到的Input.GetTouch(0).deltaPosition会不一样
                    也可在Awake()方法中 默认一个速度 speet=你调出的参考值 * Screen.width / 你的默认值。
                    开发中就不要了，因为不要测试。待程序发布的时候可以。
                    如果按我这么写，700这个值在1080P的屏幕上移动是很流畅的。
                     调试的时候就不能用这么高的值。一般在10到100之间。
                     */
                    if ((movePos.x / 700) + transform.position.x <= Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
                    {
                        moveX += movePos.x / 700;
                    }
                }
                //向左移动
                if (fristPos.x > movePos.x)
                {
                    //判断移动是否会超出屏幕左侧 (-1.2是摄像机左的临界点)  
                    if ((transform.position.x + movePos.x / 700) >= -1.2)
                    {
                        moveX -= Mathf.Abs(movePos.x / 700);
                    }
                }
            }
            //更据Y轴的值判断移动方向
            if (Mathf.Abs(fristPos.y - movePos.y) > 0.1)
            {
                //向上移动
                if (fristPos.y < movePos.y)
                {
                    //判断移动是否会超出屏幕上方
                    if ((movePos.y / 700) + transform.position.y <= Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
                    {
                        moveY += movePos.y / 700;
                    }

                }
                //向下移动
                if (fristPos.y > movePos.y)
                {
                    //判断移动是否会超出屏幕下方
                    if ((transform.position.y + movePos.y / 700) > -1.9)
                    {
                        moveY -= Mathf.Abs(movePos.y / 700);
                    }
                }
            }

            if (Mathf.Abs(fristPos.x - movePos.x) > Mathf.Abs(fristPos.y - movePos.y))
            {
                if (moveX > 0)
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * -60 * Time.deltaTime, Space.World);
                }
                else if (moveX < 0)
                {
                    transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 60 * Time.deltaTime, Space.World);
                }

            }
            else
            {
                //最后改变物体位置
                transform.Translate(new Vector3(0, 0, moveY));
                //transform.Rotate(new Vector3(0, moveX, 0));
            }

            state = PlayerState.walk;
        }
        else
        {
            state = PlayerState.idle;
        }
    }

}
