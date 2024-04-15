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
    public GameController game;
    public SimpleTouchAreaButton areaButton;
    public float fireRate;
    public float bonusFire;

    private float waveBonus;
    private float waveCount;
    private Rigidbody rb;
    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        waveCount= game.GetWaveBonus();
        if (waveCount % 5 == 0)
        {
            waveBonus = 1.5f;
        }
        else
        {
            waveBonus = 1.0f;
        }

        if ((areaButton.CanFire() && Time.time > nextFire))
        {
            nextFire = Time.time + (fireRate / waveBonus);
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }

    }

    void FixedUpdate()
    {
        rb.position = new Vector3
           (
               Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
           );

    }

}
