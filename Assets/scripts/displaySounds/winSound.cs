using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winSound : MonoBehaviour
{
    public string winSoundName = "win";


    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.Play(winSoundName);
    }


    // Update is called once per frame
    void Update()
    {       
    }
}
