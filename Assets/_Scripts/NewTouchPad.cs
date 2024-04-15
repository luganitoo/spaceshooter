using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTouchPad : MonoBehaviour
{
    private Touch touch;

    void Awake()
    {
        touch = Input.GetTouch(0);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if(touch.phase == TouchPhase.Began)
            {

            }

            if (touch.phase == TouchPhase.Moved)
            {

            }

            if (touch.phase == TouchPhase.Ended)
            {

            }
        }
    }


}
