using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int maxLifePoints = 3;
    public int lifePoints;

    public MeshRenderer rendererMarteau;
    
    public MeshRenderer rendererBook;
    public AudioSource hurtSound;
    public AudioSource deadSound;


    public SoundManager soundManager;

    private float invincibilityTimer = 2f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lifePoints = maxLifePoints;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.identity;
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DeathZombie>() != null && timer <= 0f && !collision.gameObject.GetComponent<DeathZombie>().dead)
        {
            lifePoints--;
            timer = invincibilityTimer;

            
            if(lifePoints == 0)
            {
                deadSound.Play();
                this.GetComponent<Collider>().enabled = false;
                rendererBook.transform.parent.gameObject.SetActive(false);
                rendererMarteau.transform.parent.gameObject.SetActive(false);
                StartCoroutine(soundManager.Death());
                
            }
            else
            {
                hurtSound.Play();
                StartCoroutine(blinking());
            }
        }
    }

    IEnumerator blinking()
    {
        while(timer > 0)
        {
            rendererMarteau.enabled = !rendererMarteau.enabled;
            rendererBook.enabled = !rendererBook.enabled;
            yield return new WaitForSeconds(invincibilityTimer / 20f);
        }
        rendererMarteau.enabled = true;
        rendererBook.enabled = true;
    }
}
