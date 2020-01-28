using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeathZombie : MonoBehaviour

{

    bool canDie = false;
    bool dead = false;

    public GameObject bodyPart;
    
    public float delayDisappear = 2.0f;

    Transform target;
    Animator animator;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent> ();
        agent.SetDestination (target.position);
    }

    // Update is called once per frame
    void Update () {
        if(!dead)
        {
            agent.SetDestination (target.position);
        }
        else
        {
            if(!agent.isStopped)agent.isStopped = true;
        }
        
    }

    void OnAnimatorMove ()
    {
        // Update position based on animation movement using navigation surface height
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;
    }
    /// <summary>
    /// Gère la collision
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter (Collision collision) {
        GameObject gb = collision.contacts[0].thisCollider.gameObject;
        //Si le collider n'est pas celui du zombie mais celui d'un enfant
        if(gb != this.gameObject && !collision.contacts[0].thisCollider.gameObject.name.Contains("Zombie") && collision.gameObject.tag == "Weapon")
        {
            

            if(gb.tag == "Legs")
            {
                
                CrawlZombie();
            }

            GameObject gbPart = Instantiate(bodyPart,gb.transform.position,gb.transform.rotation);
            gbPart.GetComponent<MeshFilter>().mesh = gb.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            CapsuleCollider collider= gbPart.GetComponent<CapsuleCollider>();
            collider = (CapsuleCollider) CopyComponent(gb.GetComponent<CapsuleCollider>(),gbPart);
            
            Destroy(gbPart,10);

            for(int i =0 ; i< gb.transform.childCount ; i++)
            {
                GameObject gbChild = gb.transform.GetChild(i).gameObject;

                GameObject gbPartChild = Instantiate(bodyPart,gbChild.transform.position,gbChild.transform.rotation);
                gbPartChild.GetComponent<MeshFilter>().mesh = gbChild.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                
                CapsuleCollider colliderChild = gbPartChild.GetComponent<CapsuleCollider>();
                colliderChild = (CapsuleCollider)CopyComponent(gbChild.GetComponent<CapsuleCollider>(),gbPartChild);
                Destroy(gbPartChild,10);
            }
            
            gb.SetActive(false);
            Debug.Log(collision.contacts[0].thisCollider.gameObject.name);


            if(!dead && gb.tag=="Lethal")
            {
                Debug.Log ("Die");
                dead = true;
                this.gameObject.GetComponent<Animator> ().SetTrigger ("Death");
                StartCoroutine (OnCompleteDeathAnim (this.gameObject.GetComponent<Animator> ()));
            }
        }

        Debug.Log (collision.gameObject.name);


    }

    public void DeathByFire()
    {
        Debug.Log ("Die");
        dead = true;
        this.gameObject.GetComponent<Animator> ().SetTrigger ("Death");
        StartCoroutine (OnCompleteDeathAnim (this.gameObject.GetComponent<Animator> ()));
    }

    void CrawlZombie()
    {
        animator.SetTrigger ("Crawl");
    }
    
    /// <summary>
    /// Détruit le zombie quand il meurt
    /// </summary>
    /// <param name="anim"></param>
    /// <returns></returns>
    IEnumerator OnCompleteDeathAnim (Animator anim) {
        while (anim.GetCurrentAnimatorStateInfo (0).normalizedTime < 0.99f) {
            yield return null;
        }
        yield return new WaitForSeconds (delayDisappear);
        Destroy (this.gameObject);
    }

    //Not Working
     Component CopyComponent(Component original, GameObject destination)
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            // Copied fields can be restricted with BindingFlags
            System.Reflection.FieldInfo[] fields = type.GetFields(); 
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy;
        }

}