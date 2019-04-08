using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour {
    //where does the alein go?
    public Transform target;
    //The time to wait between navigation updates
    public float updateTime;
    //need an event to signal alien death
    public UnityEvent onAlienDeath;
    //get the alien head
    public Rigidbody head;
    //flag for alien life
    public bool isAlive = true;

    //navhmesh agent
    private NavMeshAgent agent;
    //time since last update
    private float timeSinceUpdate = 0;
    //the death particles
    private DeathParticles deathParticles;

	// Use this for initialization
	void Start () {
        //get the agent for the object
        agent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {

        if (isAlive)
        {
            //tell the AI where to go if a valid target is found
            if (target != null)
            {
                timeSinceUpdate += Time.deltaTime;
                if (timeSinceUpdate > updateTime)
                {
                    agent.destination = target.position;
                    timeSinceUpdate = 0;
                }
            }
        }




    }

    //need to destroy aliens if they collide with something
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Wall"))//we dont want the walls killing the aliens
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
            die();

        }

    }

    //method to handle alien death
    public void die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        if (deathParticles)
        {
            //get the death particles and activate
            deathParticles.transform.parent = null;//disconnect the particle
            deathParticles.Activate();
        }
        //launch the head
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26, 3);
        head.GetComponent<SelfDestruct>().Initiate();
        onAlienDeath.Invoke();//signal an alien has dies
        onAlienDeath.RemoveAllListeners();//get rid of listeners
        Destroy(gameObject);
    }

    //get the death particles of the alien, this is a child object
    public DeathParticles GetDeathParticles()
    {
        if(deathParticles == null)
        {
            deathParticles = GetComponentInChildren<DeathParticles>();
        }
        return deathParticles;
    }
}
