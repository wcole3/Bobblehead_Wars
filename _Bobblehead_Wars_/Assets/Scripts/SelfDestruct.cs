using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    //script to destroy an object after a certain amoutn of time
    public float destructTime = 3.0f;

    public void Initiate()
    {
        Invoke("Boom", destructTime);
    }
    private void Boom()
    {
        Destroy(gameObject);
    }
}
