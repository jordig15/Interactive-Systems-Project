using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmaSound : MonoBehaviour
{
    public string cuinaName;
    public string alarmaSoundName = "alarmaFoc";

    private GameObject cuina;
    //private AudioSource alarmaFocSource;

    // Start is called before the first frame update
    void Start()
    {
        cuina = GameObject.Find(cuinaName);
        //alarmaFocSource = SoundManager.Instance.alarmaFocSource;

        if (cuina.tag == "active")
        {            
            SoundManager.Instance.Play(alarmaSoundName);
        }
    }

    // Update is called once per frame
    void Update()
    {       
        if(cuina.tag == "active" )
        {
            //SoundManager.Instance.playAlarmaSound();
        }
    }
}
