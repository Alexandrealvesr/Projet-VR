using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BladeHit : MonoBehaviour
{
    SteamVR_Input_Sources hand;
    public SteamVR_Action_Vibration haptic;
    // Start is called before the first frame update
    void Start()
    {
        hand = SteamVR_Input_Sources.RightHand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision collision) {
        this.gameObject.GetComponent<AudioSource>().Play();
        haptic.Execute(0f, 0.15f, 100f, 1f, hand);
    }
}
