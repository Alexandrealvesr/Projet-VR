using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeathZombie : MonoBehaviour

{

    bool dead=false;

    public Transform target;
    public float delayDisappear = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    void OnCollisionEnter(Collision other) {
     Debug.Log(other.gameObject.name);
     if(!dead && other.gameObject.name == target.name)
     {
         Debug.Log("Die");
        dead=true;
        this.gameObject.GetComponent<Animator>().SetTrigger("Death");
        StartCoroutine(OnCompleteDeathAnim(this.gameObject.GetComponent<Animator>()));
     }
     
    }

    IEnumerator OnCompleteDeathAnim(Animator anim)
    {
        while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f)
        {
            yield return null;
        }
       yield return new WaitForSeconds(delayDisappear);
        Destroy(this.gameObject);
    }
    
}
