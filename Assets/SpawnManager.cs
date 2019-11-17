using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    ZombieSpawner[] zombieSpawners;

    [SerializeField]
    uint nbSimultaneousZombies = 5;

    [SerializeField]
    float rateSpawn = 2f;

    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        zombieSpawners = FindObjectsOfType<ZombieSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= rateSpawn)
        {
            SpawnZombie();
            currentTime -= rateSpawn;
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

        if(currentZombies < nbSimultaneousZombies && spawnersAvailable.Count > 0)
        {
            int spawnIndex = Random.Range(0,spawnersAvailable.Count);
            spawnersAvailable[spawnIndex].SpawnZombie();
        }
    }
}
