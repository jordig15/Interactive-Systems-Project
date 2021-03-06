using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager Instance; //instance

    [HideInInspector]
    public GameObject redPlayer;
    [HideInInspector]
    public GameObject bluePlayer;
    
    private string redPlayerObjTag = "RedPlayerObject";    
    private string bluePlayerObjTag = "BluePlayerObject";

    //players objects
    [HideInInspector]
    public ObjectScript redPlayerObject; //contenidor per al objecte que ha agafat el jugador vermelll
    [HideInInspector]
    public ObjectScript bluePlayerObject; //contenidor per al objecte que ha agafat el jugador blau         

    void Awake()
    {        
        Instance = this;        
        redPlayer = GameObject.Find("Red Player");
        bluePlayer = GameObject.Find("Blue Player");              

        for (int i = 0; i < Object.FindObjectsOfType<ObjectScript>().Length; i++)
        {
            if(Object.FindObjectsOfType<ObjectScript>()[i].gameObject.tag == redPlayerObjTag)
            {
                redPlayerObject = Object.FindObjectsOfType<ObjectScript>()[i];
            }
            else if (Object.FindObjectsOfType<ObjectScript>()[i].gameObject.tag == bluePlayerObjTag)
            {
                bluePlayerObject = Object.FindObjectsOfType<ObjectScript>()[i];
            }
        }
    }

    void Update()
    {       
    }

    public void catchObject(string catcherPlayerName, ObjectScript obj) // quan s'agafa un objecte
    {
        GameObject catcherPlayer = GameObject.Find(catcherPlayerName);
        if (catcherPlayer == redPlayer)
        {
            redPlayerObject = obj;            
        }
        else if (catcherPlayer == bluePlayer)
        {
            bluePlayerObject = obj;            
        }

        Debug.Log("CATCHED OBJECT!" + " | Red player object: " + redPlayerObject + " | Blue player object: " + bluePlayerObject);
    }

    public void untagAllObjectsWithTag(string tagName)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
        for(int i=0;i<objs.Length; i++)
        {
            GameObject obj = objs[i];
            obj.tag = "Untagged";            
        }
    }
}
