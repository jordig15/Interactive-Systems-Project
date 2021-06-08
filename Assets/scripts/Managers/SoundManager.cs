using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip fire;
    public AudioClip extintor;
    public AudioClip water;
    public AudioClip key;
    public AudioClip doorOpening;
    public AudioClip alarmaFoc;
    public AudioClip aspiradora;
    public AudioClip startRentadora;
    public AudioClip endRentadora;

    public AudioClip backgroundSound;
    public AudioClip winSound;

    //[HideInInspector]
    //public AudioSource alarmaFocSource;

    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;

        //alarmaFocSource = GetComponent<AudioSource>();
        //Debug.Log(alarmaFocSource);

        //alarmaFocSource.clip = alarmaFoc;
        //Debug.Log(alarmaFocSource.clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playSound(AudioClip clip)
    {        
        AudioSource.PlayClipAtPoint(clip, cameraPosition);
    }

    public void playFireSound()
    {
        playSound(fire);
    }

    public void playAlarmaSound()
    {
        //playSound(alarmaFocSource.clip);
        playSound(alarmaFoc);
    }   

    public void playObjectInteractionSound(AudioClip clip)
    {
        playSound(clip);
    }


}
