using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (Input.GetKeyDown("o"))
        {
            GameObject key4 = GameObject.Find("key4Container");
            if (key4.tag == "active")
            {
                key4.tag = "not active";
                Debug.Log("PORTA 4 DESBLOQUEJADA!!");
            }
            else if (key4.tag == "not active")
            {
                key4.tag = "active";
                Debug.Log("PORTA 4 BLOQUEJADA!!");
            }
        }
        if (Input.GetKeyDown("p"))
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
        if (Input.GetKeyDown("0"))
        {
            GameObject finalKey = GameObject.Find("keyFinalContainer");
            if (finalKey.tag == "active")
            {
                finalKey.tag = "not active";
                Debug.Log("PORTA FINAL DESBLOQUEJADA!!");
            }
            else if (finalKey.tag == "not active")
            {
                finalKey.tag = "active";
                Debug.Log("PORTA FINAL DESBLOQUEJADA!!");
            }
        }
        if (Input.GetKeyDown("c"))
        {
            GameObject cuina = GameObject.Find("cuina");
            if (cuina.tag == "active")
            {
                cuina.tag = "not active";
                Debug.Log("FOC CUINA APAGAT!!");
            }
            else if (cuina.tag == "not active")
            {
                cuina.tag = "active";
                Debug.Log("FOC CUINA ENCÉS!!");
            }
        }
        if (Input.GetKeyDown("r"))
        {
            GameObject roba = GameObject.Find("roba");
            if (roba.tag == "active")
            {
                roba.tag = "not active";
                Debug.Log("TRAJE A LA RENTADORA, I RENTADORA ACABADA!!");
            }
            else if (roba.tag == "not active")
            {
                roba.tag = "active";
                Debug.Log("TRAJE NO ESTÀ A LA RENTADORA, CAL POSAR-LA!!");
            }
        }
        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("1st room");
        }
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("2n room");
        }
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("3rd room");
        }
        if (Input.GetKeyDown("4"))
        {
            SceneManager.LoadScene("4th room");
        }
        if (Input.GetKeyDown("5"))
        {
            SceneManager.LoadScene("5th room");
        }
    }
}
