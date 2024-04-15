using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTouchPad : MonoBehaviour {

    public float smoothing;
    private Vector2 smoothDirection;
    private Vector2 direction;
    private Vector2 currentPosition;
    private Touch touch;

    void Awake()
    {
        direction = Vector2.zero;
        touch = Input.GetTouch(0);
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {

            if (touch.phase == TouchPhase.Moved) {


                direction = touch.deltaPosition.normalized;

            }

        }
    }


    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        return smoothDirection;
    }

}

//newMethod Touch input
//currentPosition.x = Input.GetTouch(0).position.x - pastPosition.x;
//            currentPosition.y = Input.GetTouch(0).position.y - pastPosition.y;
//            pastPosition = Input.GetTouch(0).position;

//            direction = currentPosition.normalized;