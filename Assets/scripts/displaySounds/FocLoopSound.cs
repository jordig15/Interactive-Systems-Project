using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocLoopSound : MonoBehaviour
{
    public string cuinaName;
    public string focLoopSoundName = "focLoop";

    private GameObject cuina;
    //private AudioSource alarmaFocSource;

    // Start is called before the first frame update
    void Start()
    {
        cuina = GameObject.Find(cuinaName);        

        if (cuina.tag == "active")
        {            
            SoundManager.Instance.Play(focLoopSoundName);
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if(cuina.tag == "not active")
        {
            if (SoundManager.Instance.IsPlaying(focLoopSoundName))
            {
                SoundManager.Instance.Stop(focLoopSoundName);   
            }
        }
    }
}
