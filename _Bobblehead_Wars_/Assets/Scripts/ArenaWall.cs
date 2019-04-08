using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use this to control the arena wall animations
public class ArenaWall : MonoBehaviour {
    private Animator arenaAnimator;
	// Use this for initialization
	void Start () {
        //get the arena
        GameObject arena = transform.parent.gameObject;
        //get the animator
        arenaAnimator = arena.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //want use triggers to raise and lower walls
    private void OnTriggerEnter(Collider other)
    {
        arenaAnimator.SetBool("LowerWall", true);
               
    }

    private void OnTriggerExit(Collider other)
    {
        arenaAnimator.SetBool("LowerWall", false);
             
    }
}
