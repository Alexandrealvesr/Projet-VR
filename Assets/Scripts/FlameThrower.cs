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

    private bool hold = false;

    private float timeHold = 0f;

    private GameObject gb = null;
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

        if(gb == null)
        {
            hold = false;
            timeHold = 0f;
        }
        if(hold)
        {
            timeHold += Time.deltaTime;
            gb.transform.localScale = new Vector3(1, 1, 1) * Mathf.Min(1, timeHold / 0.5f);
        }

        if(benzin.GetStateDown(SteamVR_Input_Sources.LeftHand) && elapsedTime > secondsCooldown)
        {
            hold = true;
            if(gb != null)
            {
                Destroy(gb);
                gb = null;
            }
            StartBall();
        }

        if (benzin.GetStateUp(SteamVR_Input_Sources.LeftHand) && elapsedTime > secondsCooldown && hold && timeHold > 0.5f)
        {
            LaunchBall();
            elapsedTime = 0f;
            hold = false;
            timeHold = 0;
        }
    }

    public void StartBall()
    {
        gb = Instantiate(flameBallPrefab, this.point.position, this.point.rotation, this.point);
        gb.transform.localScale = Vector3.zero;
    }

    public void LaunchBall()
    {
        if(gb != null)
        {
            gb.transform.localScale = new Vector3(1, 1, 1);
            gb.transform.parent = null;
            gb.GetComponent<Rigidbody>().AddForce(this.transform.up * 1000);
            gb = null;
        }
       
    }
}
