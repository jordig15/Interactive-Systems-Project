using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focSound : MonoBehaviour
{
    public string firePlaceName;
    public string firePlaceSound = "fireplace";

    private GameObject firePlace;
    // Start is called before the first frame update
    void Start()
    {
        firePlace = GameObject.Find(firePlaceName);
        if(firePlace == null)
        {
            Debug.Log("ERROR LOADING FIREPLACE CONTAINER");
        }

        if(firePlace.tag == "active")
        {
            SoundManager.Instance.Play(firePlaceSound);
        }
    }

    // Update is called once per frame
    void Update()
    {       
    }
}
