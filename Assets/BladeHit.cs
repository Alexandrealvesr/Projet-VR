using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BladeHit : MonoBehaviour
{
    SteamVR_Input_Sources hand;
    public SteamVR_Action_Vibration haptic;

    public AudioClip hitZombie;

    public List<AudioClip> hitGround;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        hand = SteamVR_Input_Sources.RightHand;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision collision) {

        float magnitude = Mathf.Min(collision.relativeVelocity.magnitude  / 5f, 1) ;
        Debug.Log(collision.gameObject.name + ": HIT MAGNITUDE : " + magnitude + "|" + collision.relativeVelocity);
        if(collision.gameObject.name.Contains("Zombie"))
        {
            audioSource.clip = hitZombie;
        }
        else
        {
            if(hitGround != null && hitGround.Count > 0)
            {
                
                audioSource.clip = hitGround[Random.Range(0,hitGround.Count - 1)];
            }
            //Else ne devrait pas arriver, bsx
        }
        audioSource.volume =magnitude;
        audioSource.Play();
        haptic.Execute(0f, 0.15f, 100f, magnitude , hand);
    }
}
