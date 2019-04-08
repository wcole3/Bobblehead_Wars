using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    //speed constant and gameobject target
    public GameObject followTarget;
    public float moveSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//check if there is a folow target
        if(followTarget != null)
        {
            //rotate
            transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.transform.rotation, Time.deltaTime * 2.0f);
            //need to follow the target
            transform.position = Vector3.Lerp(transform.position, followTarget.transform.position, moveSpeed * Time.deltaTime);



        }
    }
}
