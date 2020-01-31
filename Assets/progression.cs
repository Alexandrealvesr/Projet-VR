using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progression : MonoBehaviour
{
    public int noIndicator;
    public int nbIndicators;
    public SpawnManager sm;
    bool starting = true;
    bool lit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sm.delayUntilStart <= 0)
        {
            starting = false;
        }
        if (starting)
        {
            if ((sm.delayUntilStart - 0.4f) / 24 >= noIndicator / (float)nbIndicators)
            {
                this.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                this.GetComponent<Renderer>().enabled = false;
            }
        }
        else
        {
            if (!lit)
            {
                if (sm.zombiesSpawned / (float)sm.nbZombiesToSpawn >= noIndicator / (float)nbIndicators)
                {
                    this.GetComponent<Renderer>().enabled = true;
                    this.GetComponent<AudioSource>().Play();
                    lit = true;
                }
                else
                {
                    this.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }
}
