using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchmov : MonoBehaviour
{

    private Vector3 pos;
    private Vector3 touch;
    public float tilt;

    public float releaseAdjuster;
    public float heightAdjuster;
    public float movedSmoothing;
    public float stationarySmoothing;
    public float endedSmoothing;
    public float drag;

    private Quaternion startRotation;

    // Use this for initialization
    void Start()
    {
        //Input.simulateMouseWithTouches = false;
        startRotation = Quaternion.Euler(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
    }

    void MoveUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touchstatus = Input.GetTouch(0);

            if(touchstatus.phase == TouchPhase.Began)
            {
                //
            }

            if (touchstatus.phase == TouchPhase.Moved)
            {

                touch = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y + heightAdjuster, 1);
                pos = Camera.main.ScreenToWorldPoint(touch);
                Vector3 currentPosition = transform.position;

                transform.position = Vector3.Lerp(currentPosition, pos, releaseAdjuster);
                Vector3 tiltvector = new Vector3(0.0f, 0.0f, 1);

                if (pos.x+drag > currentPosition.x)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -tilt), Time.deltaTime * movedSmoothing);
                    //transform.Rotate(tiltvector * -tilt, Space.World);
                }
                if (pos.x-drag < currentPosition.x)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, tilt), Time.deltaTime * movedSmoothing);
                    //transform.Rotate(tiltvector * tilt, Space.World);
                }
                
            }
        

            if(touchstatus.phase == TouchPhase.Stationary)
            {

                touch = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y + heightAdjuster, 1);
                pos = Camera.main.ScreenToWorldPoint(touch);
                Vector3 currentPosition = transform.position;

                transform.position = Vector3.Lerp(currentPosition, pos, releaseAdjuster);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), stationarySmoothing);
            }



        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * endedSmoothing);
        }
    }


}
