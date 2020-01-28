using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    [SerializeField]
    private float secondsCooldown;

    [SerializeField]
    private GameObject flameBallPrefab;

    [SerializeField]
    private Transform point;

    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.F) && elapsedTime >secondsCooldown)
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
