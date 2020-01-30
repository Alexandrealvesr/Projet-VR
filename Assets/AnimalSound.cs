using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSound : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        StartCoroutine(MovingSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MovingSound()
    {
        while(Application.isPlaying)
        {
            yield return new WaitForSeconds(Random.Range(4f, 25f));
            Vector3 pos = Random.onUnitSphere * 30;
            pos.y = 0;
            this.transform.localPosition = pos;
            
            audioSource.Play();
            
        }
    }
}
