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
            Debug.Log(GameStateManager.Instance.playersInContact);
            Debug.Log("------------");
        }
        if (Input.GetKeyDown("m"))
        {
            Debug.Log(GameStateManager.Instance.playersInContact);
            Debug.Log("------------");
        }
        if (Input.GetKeyDown("4"))
        {
            GameObject key4 = GameObject.Find("key4Container");
            if(key4.tag == "active")
            {
                key4.tag = "not active";
                Debug.Log("PORTA 4 DESBLOQUEJADA!!");
            }
            else if(key4.tag == "not active")
            {
                key4.tag = "active";
                Debug.Log("PORTA 4 BLOQUEJADA!!");
            }           
        }
        if (Input.GetKeyDown("5"))
        {
            GameObject key4 = GameObject.Find("key5Container");
            if (key4.tag == "active")
            {
                key4.tag = "not active";
                Debug.Log("PORTA 5 DESBLOQUEJADA!!");
            }
            else if (key4.tag == "not active")
            {
                key4.tag = "active";
                Debug.Log("PORTA 5 BLOQUEJADA!!");
            }
        }
    }
}
