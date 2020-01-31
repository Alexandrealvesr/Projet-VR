using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    ZombieSpawner[] zombieSpawners;

    [SerializeField]
    public float delayUntilStart = 10;

    [SerializeField]
    public uint nbZombiesToSpawn = 80;
    [SerializeField]
    uint nbSimultaneousZombies = 5;

    [SerializeField]
    float rateSpawn = 2f;

    float currentTime = 0;

    public uint zombiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        zombieSpawners = FindObjectsOfType<ZombieSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(delayUntilStart > 0)
        {
            delayUntilStart -= Time.deltaTime;
        }
        else
        {
            currentTime += Time.deltaTime;
            if(currentTime >= rateSpawn)
            {
                SpawnZombie();
                Difficulty();
                currentTime -= rateSpawn;
            }   
        }
        
    }
    public void Difficulty()
    {
        if(zombiesSpawned < 0.25f * nbZombiesToSpawn)
        {
            rateSpawn = 1;
            nbSimultaneousZombies = 5;
        }
        else if (zombiesSpawned < 0.5f * nbZombiesToSpawn)
        {
            rateSpawn = 0.5f;
            nbSimultaneousZombies = 13;
        }
        else if (zombiesSpawned < 0.75f * nbZombiesToSpawn)
        {
            rateSpawn = 0.2f;
            nbSimultaneousZombies = 18;
        }
        else 
        {
            rateSpawn = 0.075f;
            nbSimultaneousZombies = 25;
        }
    }
    public void SpawnZombie()
    {
        uint currentZombies = 0;
        List<ZombieSpawner> spawnersAvailable = new List<ZombieSpawner>();
        foreach(ZombieSpawner spawner in zombieSpawners)
        {
            if(spawner.IsZombieAlive())currentZombies++;
            else
            {
                spawnersAvailable.Add(spawner);
            }
        }

        if(currentZombies < nbSimultaneousZombies && spawnersAvailable.Count > 0 && zombiesSpawned < nbZombiesToSpawn )
        {
            int spawnIndex = Random.Range(0,spawnersAvailable.Count);
            spawnersAvailable[spawnIndex].SpawnZombie();
            zombiesSpawned++;
        }
    }
}
