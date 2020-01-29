using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FlameThrower : MonoBehaviour
{
    [SerializeField]
    private float secondsCooldown;

    [SerializeField]
    private GameObject flameBallPrefab;

    [SerializeField]
    private Transform point;

    public SteamVR_Action_Boolean benzin;


    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(benzin.GetStateDown(SteamVR_Input_Sources.LeftHand))
        Debug.Log(benzin.GetStateDown(SteamVR_Input_Sources.LeftHand));
        if(benzin.GetStateDown(SteamVR_Input_Sources.LeftHand) && elapsedTime >secondsCooldown)
        {
            LaunchBall();
            elapsedTime = 0f;
        }
    }

    public void LaunchBall()
    {
        GameObject gb = Instantiate(flameBallPrefab, this.point.position, this.point.rotation);
        gb.GetComponent<Rigidbody>().AddForce(this.transform.up * 1000);
    }
}
