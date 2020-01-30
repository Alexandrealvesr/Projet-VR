using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int lifePoints = 3;

    public MeshRenderer rendererMarteau;

    private float invincibilityTimer = 2f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("AIE");
            timer = invincibilityTimer;

            StartCoroutine(blinking());
            if(lifePoints == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator blinking()
    {
        while(timer > 0)
        {
            rendererMarteau.enabled = !rendererMarteau.enabled;
            yield return new WaitForSeconds(invincibilityTimer / 20f);
        }
        rendererMarteau.enabled = true;
    }
}
