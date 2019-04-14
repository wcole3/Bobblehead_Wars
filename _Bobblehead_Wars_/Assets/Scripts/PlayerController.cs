using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameUI gameUI;
    //speed constant
    public float moveSpeed = 50.0f;
    //get the marine body
    public Rigidbody marineBody;
    //get the animator
    public Animator playerAnimator;
    //get reference to the head
    public Rigidbody head;
    //need references to use for raycasting
    public LayerMask rayLayerMask;
    //the amoount of force/damage per alien hit
    public float[] hitForce;
    //the amount of time between hits
    public float timeBetwHits = 2.5f;

    //the direction the mouse it relative the the players face
    private Vector3 mouseLookDirection = Vector3.zero;
    //variables to control incoming hits
    private bool isHit = false;
    private float timeSinceHit = 0;
    private int hitNumber = -1;
    //get reference to player controller
    private CharacterController characterController;
    //bool flag for marine death
    private bool isDead = false;
    //death particles
    private DeathParticles marineParticles;
    // Use this for initialization
    void Start()
    {
        //get the character controller
        characterController = GetComponent<CharacterController>();
        marineParticles = GetComponentInChildren<DeathParticles>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        //check for damage
        if (isHit)
        {
            timeSinceHit += Time.deltaTime;
            if(timeSinceHit > timeBetwHits)
            {
                timeSinceHit = 0;
                isHit = false;
            }
        }
        //get the vector descibing the movement
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        //now move
        characterController.SimpleMove(moveDirection * moveSpeed);
    }
    //need a fixed upadte for head physics
    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //check if there is movement
        if (moveDirection == Vector3.zero)
        {
            playerAnimator.SetBool("IsMoving", false);
        }
        else
        {
            //player is moving
            playerAnimator.SetBool("IsMoving", true);
            //add force to head to make it bobble
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }

        //need to do the ray casting here
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //get the ray that is from the camera to the mouse 2d position
        //we also want to draw the ray
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green);

        //cast the ray to hit
        if (Physics.Raycast(ray, out hit, 1000, rayLayerMask, QueryTriggerInteraction.Ignore))
        {
            // change the mouse look direction mouse position
            if (hit.point != mouseLookDirection)
            {
                mouseLookDirection = hit.point;
            }
        }
        //we will use that to reorient the player
        Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        Quaternion rotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10.0f);


    }

    //handle collision events
    private void OnTriggerEnter(Collider other)
    {
        Alien alien = other.gameObject.GetComponent<Alien>();
        if(alien != null)
        {
            //if alien exists
            if (!isHit)
            {
                //if not hit
                ++hitNumber;
                //update the UI
                gameUI.DisableHealthBox(hitNumber);
                //shake camera
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if(hitNumber < hitForce.Length)
                {
                    cameraShake.intensity = hitForce[hitNumber];
                    cameraShake.Shake();//shake the screen
                }
                else
                {
                    die();
                }
                //the marine has been hit
                isHit = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);
            }
            //since the alien hit the player they die
            alien.die();
        }
    }
    //method to call on marine death
    public void die()
    {
        isDead = true;
        playerAnimator.SetBool("IsMoving", false);
        //disconect the body
        marineBody.transform.parent = null;
        marineBody.isKinematic = false;//ragdoll
        marineBody.useGravity = true;
        //enable the collider
        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        marineBody.gameObject.GetComponent<Gun>().enabled = false;//no more shooting
        //remove head
        Destroy(head.gameObject.GetComponent<HingeJoint>());
        head.transform.parent = null;
        head.useGravity = true;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.marineDeath);
        //spill blood
        marineParticles.Activate();
        Destroy(gameObject);

    }
}
