using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focSound : MonoBehaviour
{
    public string firePlaceName;

    private GameObject firePlace;
    // Start is called before the first frame update
    void Start()
    {
        firePlace = GameObject.Find(firePlaceName);

        if(firePlace.tag == "active")
        {
            SoundManager.Instance.playFireSound();
        }
    }

    // Update is called once per frame
    void Update()
    {       
    }
}
