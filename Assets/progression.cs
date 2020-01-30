using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progression : MonoBehaviour
{
    public int noIndicator;
    public int nbIndicators;
    public SpawnManager sm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.zombiesSpawned / (float)sm.nbZombiesToSpawn >= noIndicator / (float)nbIndicators)
        {
            this.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }
}
