using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager Instance; //instance

    private GameObject redPlayer;
    private GameObject bluePlayer;
    
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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

        Debug.Log("CATCHED OBJECT!" + ", " + redPlayerObject + " | " + bluePlayerObject);
    }

    public void untagAllObjectsWithTag(string tagName)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
        for(int i=0;i<objs.Length; i++)
        {
            GameObject obj = objs[i];
            obj.tag = "Untagged";
            Debug.Log(obj.name + " | Removed tag");           
        }
    }
}
