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
    private Material bookMat;

    [SerializeField]
    private Material flameMat;

    [SerializeField]
    private Transform point;

    public SteamVR_Action_Boolean benzin;


    private float elapsedTime;

    private bool hold = false;

    private float timeHold = 0f;

    private GameObject gb = null;

    public AudioSource holdHandler;
    public AudioSource throwHandler;
    public AudioSource notEnoughManaHandler;
    public AudioSource spellReadyHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(!hold)bookMat.SetFloat("Vector1_6482DDD8",0.5f * Mathf.Min(1, elapsedTime / secondsCooldown));

        if(gb == null)
        {
            hold = false;
            timeHold = 0f;
        }
        if(hold)
        {
            timeHold += Time.deltaTime;
            gb.transform.localScale = new Vector3(1, 1, 1) * Mathf.Min(1, timeHold / 1f);
            flameMat.SetFloat("Vector1_6482DDD8",0.3f * Mathf.Min(1, timeHold / 1f));
            bookMat.SetFloat("Vector1_6482DDD8",0.5f * (1 - Mathf.Min(1, elapsedTime / secondsCooldown)));
        }

        if(benzin.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if (elapsedTime > secondsCooldown) {

                hold = true;
                if (gb != null)
                {
                    Destroy(gb);
                    gb = null;
                }
                StartBall();
            }
            else
            {
                notEnoughManaHandler.Play();
            }
        }

        if (benzin.GetStateUp(SteamVR_Input_Sources.LeftHand) && elapsedTime > secondsCooldown && hold && timeHold > 1f)
        {
            LaunchBall();
            elapsedTime = 0f;
            hold = false;
            timeHold = 0;
        }
    }

    public void StartBall()
    {
        gb = Instantiate(flameBallPrefab, new Vector3(this.point.position.x+0.01f, this.point.position.y+0.05f, this.point.position.z), this.point.rotation, this.point);
        gb.transform.localScale = Vector3.zero;
        gb.GetComponent<Rigidbody>().isKinematic= true;
        holdHandler.mute = false;
    }

    public void LaunchBall()
    {
        if(gb != null)
        {
            gb.GetComponent<Rigidbody>().isKinematic = false;
            gb.transform.localScale = new Vector3(1, 1, 1);
            gb.transform.parent = null;
            gb.GetComponent<Rigidbody>().AddForce(this.transform.up * 1000);
            gb = null;
        }
        holdHandler.mute = true;
        throwHandler.Play();
        spellReadyHandler.Play();

    }
}
