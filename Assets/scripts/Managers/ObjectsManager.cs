using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager Instance; //instance

    private GameObject redPlayer;
    private GameObject bluePlayer;

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

    /*public bool playerHasObject(string playerName)
    {
        if(playerName == redPlayer.name)
        {
            if (redPlayerObject != null) return true;
        }
        else if(playerName == bluePlayer.name)
        {
            if (bluePlayerObject != null) return true;
        }

        return false;
    }*/

}
