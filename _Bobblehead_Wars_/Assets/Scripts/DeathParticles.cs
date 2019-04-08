using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour {
    //get the patricle system
    private ParticleSystem deathParticles;
    //check if the effect has started
    private bool didStart = false;

	// Use this for initialization
	void Start () {
        deathParticles = GetComponent<ParticleSystem>();
	}
	
    //activate the particles
    public void Activate()
    {
        didStart = true;
        deathParticles.Play();
    }

	// Update is called once per frame
	void Update () {
		//check if the particle effect has complete
        if(didStart && deathParticles.isStopped)
        {
            Destroy(gameObject);
        }
	}

    //need to be able to get the death floor
    public void setDeathFloor(GameObject deathFloor)
    {
        if (deathParticles == null)
        {
            //if there are no particles make some
            deathParticles = GetComponent<ParticleSystem>();
        }

        //set the death floor
        deathParticles.collision.SetPlane(0, deathFloor.transform);
    }
}
