using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {
    //get the player and elevator
    public GameObject player;
    public Transform elevator;

    //animator and collider
    private Animator arenaAnimator;
    private SphereCollider arenaCollider;

	// Use this for initialization
	void Start () {
        arenaAnimator = GetComponent<Animator>();
        arenaCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //if the player enters the collider the game is over
    private void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false;//stop camera movement
        Camera.main.GetComponent<CameraShake>().enabled = false;
        player.transform.parent = elevator.transform;//player becomes child of elevator
        player.GetComponent<PlayerController>().enabled = false;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
        //start animation
        arenaAnimator.SetBool("OnElevator", true);

    }
    //need to activate elevator on victory
    public void ActivateElevator()
    {
        arenaCollider.enabled = true;

    }
}
