using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () { 
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, (Random.Range(1.0f, 2.0f) * fireRate));

    }

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }

}
