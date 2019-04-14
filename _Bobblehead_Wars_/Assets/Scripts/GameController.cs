using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    //get the game UI
    [SerializeField]
    GameUI gameUI;
    //get player, spawnpoints, and alien
    public GameObject player;
    public GameObject[] spawnPoints;
    public GameObject alien;
    //upgrade prefab
    public GameObject upgradePrefab;
    //max time to wait before spwaning the upgrade
    public float maxUpgradeSpawnTime = 7.5f;
    //the gun script
    public Gun gun;
    //variables to control game conditions
    public int totalAliens;//total number of aliens need to win game
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;
    public int maxAlienOnScreen;
    //get the death floor
    public GameObject deathFloor;
    //the deat particles
    public GameObject AlienDeathParticles;
    //get arena animator
    public Animator arenaAnimator;
    //private variables
    private float generatedSpawnTime = 2.0f;
    private float currentSpawnTime = 0;
    private int aliensOnScreen = 0;
    //upgrade spawn controls
    private bool upgradeSpwaned = false;
    private float currentUpgradeTime = 0;
    private float actualUpgradeTime = 0;
    private int aliensNeededForWin;
	// Use this for initialization
	void Start () {
        //get the time to wait before spawning the upgrade
        actualUpgradeTime = Random.Range(maxUpgradeSpawnTime - 3.0f, maxUpgradeSpawnTime);
        //make sure its positive
        actualUpgradeTime = Mathf.Abs(actualUpgradeTime);
        //set the game UI
        gameUI.SetEnemyText(totalAliens);
	}
	
	// Update is called once per frame
	void Update () {
        //only do things if player is alive
        if(player == null)
        {
            return;
        }
        //we need to spawn the aliens
        currentSpawnTime += Time.deltaTime;
        currentUpgradeTime += Time.deltaTime;
        if(currentSpawnTime > generatedSpawnTime)
        {
            //we need to spawn
            //first reset timer
            currentSpawnTime = 0;
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            //now check if we need we can make more aliens
            if(aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
            {
                //get a list of the spawnpoints
                List<int> previousSpawnPoints = new List<int>();
                //make sure aliens per spawn isnt more than available spawn points
                if(aliensPerSpawn > spawnPoints.Length)
                {
                    aliensPerSpawn = spawnPoints.Length - 1;
                }
                //now check if adding spawning the aliens will go over the total aliens
                aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn
                    - totalAliens : aliensPerSpawn;
                //now that aliens per spawn is set we can choose spawn points
                for(int i = 0; i < aliensPerSpawn; ++i)
                {
                    //check if we have reached the cap
                    if(aliensOnScreen < maxAlienOnScreen)
                    {
                        aliensOnScreen += 1;
                        //get spawn point
                        int index = -1;
                        while(index == -1)
                        {
                            int randomIndex = Random.Range(0, spawnPoints.Length - 1);
                            if (!previousSpawnPoints.Contains(randomIndex))
                            {
                                //if we haven't used the spawn point we can use 
                                previousSpawnPoints.Add(randomIndex);
                                index = randomIndex;

                            }
                        }
                        //now that we know where to spawn we can create the alien
                        GameObject spawnLocation = spawnPoints[index];
                        GameObject spawnedAlien = Instantiate(alien);
                        //set the starting loaction of the alien
                        spawnedAlien.transform.position = spawnLocation.transform.position;
                        //setup the aliens ai
                        Alien alienScript = spawnedAlien.GetComponent<Alien>();
                        alienScript.target = player.transform;//set the player as the target
                                                              //rotate the alien to face the player
                        Vector3 targetRotation = new Vector3(player.transform.position.x, spawnedAlien.transform.position.y,
                                                    player.transform.position.z);
                        spawnedAlien.transform.LookAt(targetRotation);
                        //add a event listener to the alien
                        alienScript.onAlienDeath.AddListener(AlienKilled);
                        //set the death floor
                        alienScript.GetDeathParticles().setDeathFloor(deathFloor);



                    }

                }
            }
        }
        //perform upgrade spawing
        if(currentUpgradeTime > actualUpgradeTime && !upgradeSpwaned)
        {
            //spawn the upgrade
            currentUpgradeTime = 0;//reset timer
            //get a spwan point 
            int spawnPoint = Random.Range(0, spawnPoints.Length - 1);
            GameObject upgrade = (GameObject)Instantiate(upgradePrefab);
            upgrade.transform.position = spawnPoints[spawnPoint].transform.position;
            //set the upgrade script and the gun script
            Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
            upgradeScript.gun = gun;
            //play upgrade spawn sound
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpSpawn);
            //mark upgrade as spawned
            upgradeSpwaned = true;
        }
    }

    //method to handle alien death
    public void AlienKilled()
    {
        //we have killed an alien
        --totalAliens;
        --aliensOnScreen;
        //check if game is over
        if(totalAliens == 0)
        {
            Invoke("EndGame", 2.0f);
        }
        //update the UI
        gameUI.SetEnemyText(totalAliens);
    }
    //end the game if player won
    private void EndGame()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.victory);
        arenaAnimator.SetTrigger("PlayerWon");
    }
}
