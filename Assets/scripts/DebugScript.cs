using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("RED PLAYER OBJECT: " + ObjectsManager.Instance.redPlayerObject);
            Debug.Log("BLUE PLAYER OBJECT: " + ObjectsManager.Instance.bluePlayerObject);
            Debug.Log("------------");
        }
    }
}
