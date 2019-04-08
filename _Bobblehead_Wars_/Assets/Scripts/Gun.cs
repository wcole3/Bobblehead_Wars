using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    //need to know what to fire and the place to launch from
    public GameObject bulletPrefab;
    public Transform launcherPos;
    //add funtionality to upgrade gun
    public bool isUpgraded;

    //sound effect to play on gun shoot
    private AudioSource audioSource;
    private float currentTime; //current time
    private float upgradeTime = 10.0f;//time limit for upgrade

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //check if we are already firing
            if (!IsInvoking("fireBullet"))
            {
                //then we can fire with a delay
                InvokeRepeating("fireBullet", 0f, 1.0f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }
        //check upgrade status
        if (isUpgraded)
        {
            currentTime += Time.deltaTime;
            if(currentTime > upgradeTime)
            {
                isUpgraded = false;
                currentTime = 0;
            }
        }
    }
    //method to fire bullets
    void fireBullet()
    {
        Rigidbody bullet = GetBullet();
        //set bullet velocity
        bullet.velocity = transform.parent.forward * 100;
        //if the gun is upgraded, fire two more
        if (isUpgraded)
        {
            Rigidbody bullet2 = GetBullet();
            bullet2.velocity = ((transform.parent.forward * 2) + transform.parent.right) * 50;
            Rigidbody bullet3 = GetBullet();
            bullet3.velocity = ((transform.parent.forward * 2) + (transform.parent.right * -1)) * 50;
            //play sound
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.Instance.gunfire);
        }


    }
    //method to create a bullet from pool
    private Rigidbody GetBullet()
    {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet");
        if(bullet != null)
        {
            //set bullets position
            bullet.transform.position = launcherPos.position;
            //set active
            bullet.SetActive(true);
            return bullet.GetComponent<Rigidbody>();
        }
        return null;
    }

    //method to upgrade the gun
    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }
}
