using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButton areaButton;
    public GameController game;
    public float fireRate;
    public float bonusFire;

    private int waveCount;
    private Rigidbody rb;
    private float nextFire;
    private Quaternion calibrationQuaternion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CalibrateAccelerometer();
    }

    private void Update()
    {
        waveCount = game.GetWave();
        if (waveCount % 10 == 0)
        {
            if (areaButton.CanFire() && Time.time > nextFire)
            {
                nextFire = Time.time + (fireRate / bonusFire);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (areaButton.CanFire() && Time.time > nextFire)
            {
                nextFire = Time.time + (fireRate);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                GetComponent<AudioSource>().Play();
            }
        }
        Debug.LogWarning(waveCount);
    }

    ////Used to calibrate the Iput.acceleration input
    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    //Get the 'calibrated' value from the Input
    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    void FixedUpdate()
    {
   
        //Keyboard commands
        //float moveHorizontal =  Input.GetAxis("Horizontal");
        //float moveVertical   =  Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Gyroscope commands
        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);
        //Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);


        rb.position = new Vector3
           (
               Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
           );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        Vector2 direction = touchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        rb.velocity = movement * speed;
                
             
        //if (Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    float step = stop * Time.deltaTime;
        //    rb.rotation = Quaternion.Lerp(Quaternion.identity, rb.rotation, step);
        //    rb.velocity = Vector3.MoveTowards(Vector3.zero, rb.velocity, step);
        //    rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref velocity, smooth);
        //}

        
    }

}
