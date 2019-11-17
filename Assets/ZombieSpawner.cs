using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    public GameObject prefabZombie;

    GameObject zombieSpawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnZombie()
    {
        zombieSpawned = Instantiate(prefabZombie, this.transform.position, this.transform.rotation);
    }

    /// <summary>
    /// On peut toujours changer pour mettre plusieurs zombie par spawn, à voir comment on équilibre.
    /// </summary>
    /// <returns></returns>
    public bool IsZombieAlive()
    {
        return zombieSpawned != null;
    }
}
