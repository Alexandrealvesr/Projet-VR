using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBall : MonoBehaviour
{
    bool shot = true;
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
            
            if (shot) this.transform.localScale *= 30f;
            else
            {
                Destroy(this.gameObject, 0.05f);
            }
            shot = false;
            
           this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
