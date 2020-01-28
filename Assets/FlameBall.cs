﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.GetComponent<DeathZombie>() != null )
        {
            other.gameObject.GetComponent<DeathZombie>().DeathByFire();
            Destroy(this.gameObject);
        }
    }
}
