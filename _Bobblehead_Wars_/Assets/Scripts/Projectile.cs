using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //need to add behavior to cleanup particles
    private void OnCollisionEnter(Collision collision)
    {
        //return to object pool
        gameObject.SetActive(false);
    }
    private void OnBecameInvisible()
    {
        //return to object pool
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
