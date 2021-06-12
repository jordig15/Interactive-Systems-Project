using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiguaSound : MonoBehaviour
{
    public string AiguaName;
    public string waterSoundName = "water";

    private GameObject aigua;
    //private AudioSource alarmaFocSource;

    // Start is called before the first frame update
    void Start()
    {
        aigua = GameObject.Find(AiguaName);        

        if (aigua.tag == "active")
        {            
            SoundManager.Instance.Play(waterSoundName);
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if(aigua.tag == "active" )
        {            
        }
    }
}
