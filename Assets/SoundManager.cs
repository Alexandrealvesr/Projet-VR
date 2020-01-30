using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SoundManager : MonoBehaviour
{

    public AudioSource evilLaugh;

    public PostProcessVolume postProcessVolume = null;

    ColorGrading colorGrading = null;
    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume.profile.TryGetSettings(out colorGrading);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Death()
    {
        evilLaugh.Play();
        float timer = evilLaugh.clip.length + 1;
        float currentTimer = 0;
        colorGrading.active = true;
        while(currentTimer < timer)
        {   
            currentTimer += 0.1f;
            colorGrading.saturation.value = Mathf.Max(-100 , -100 * 2 * ( currentTimer / timer));
            colorGrading.contrast.value = Mathf.Max(-100 , -100 *  ( currentTimer / timer));
            yield return new WaitForSeconds(0.1f);
        }
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
