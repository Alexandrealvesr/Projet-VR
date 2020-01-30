using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obelisk : MonoBehaviour
{
    public SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x, 0.7f * spawnManager.zombiesSpawned / spawnManager.nbZombiesToSpawn, this.transform.localScale.z);
    }
}
