using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public bool canAlwaysBeCatched = true;
    public string canBeCatchedObjName;
    private GameObject canBeCatchedObj;

    private GameObject DontDestroyEntities;

    private Vector3 objectPlayerDisp = new Vector3(-10.0f, 10.0f, -5.0f);

    private float cooldown = 2.0f;
    private float coliderPlayerDist = 15.0f;

    private string redPlayerName = "Red Player";
    private string bluePlayerName = "Blue Player";

    private string redPlayerObjTag = "RedPlayerObject";    
    private string bluePlayerObjTag = "BluePlayerObject";

    //sound
    public bool catchSound = false;
    public string catchSoundName;

    [HideInInspector]
    public GameObject catcherPlayer;
    float firstTime;    


    // Start is called before the first frame update
    void Awake()
    {        
        controlDuplicateObjects();
        getCatcherPlayer();

        DontDestroyEntities = GameObject.Find("DontDestroyEntities");        
        firstTime = -1.0f;        
    }

    void Start()
    {
        canBeCatchedObj = GameObject.Find(canBeCatchedObjName);

        catcherPlayer = null;
        getCatcherPlayer();        
    }

    void controlDuplicateObjects() //cuenta el nº de objetos duplicados en la escena y los elimina
    {
        int objectsCounter = 0;

        for (int i = 0; i < Object.FindObjectsOfType<ObjectScript>().Length; i++)
        {
            GameObject obj = Object.FindObjectsOfType<ObjectScript>()[i].gameObject;
            if (obj.name == this.gameObject.name) objectsCounter++;
        }


        for (int i = 0; i < Object.FindObjectsOfType<ObjectScript>().Length; i++)
        {
            GameObject obj = Object.FindObjectsOfType<ObjectScript>()[i].gameObject;
            if (obj.name == this.gameObject.name)
            {
                if (obj.tag != redPlayerObjTag && obj.tag != bluePlayerObjTag && objectsCounter > 1)
                {
                    Destroy(gameObject);                    
                }
            }
        }
    }

    void getCatcherPlayer()
    {
        for (int i = 0; i < Object.FindObjectsOfType<ObjectScript>().Length; i++)
        {
            if (Object.FindObjectsOfType<ObjectScript>()[i] == this)
            {
                if(Object.FindObjectsOfType<ObjectScript>()[i].gameObject.tag == redPlayerObjTag)
                {
                    catcherPlayer = ObjectsManager.Instance.redPlayer;
                }
                else if (Object.FindObjectsOfType<ObjectScript>()[i].gameObject.tag == bluePlayerObjTag)
                {                    
                    catcherPlayer = ObjectsManager.Instance.bluePlayer;                    
                }
            }                                        
        }
    }

    void Update()
    {
        if (canAlwaysBeCatched)
        {
            UpdateObject();
        }
        else if(canBeCatchedObj.tag == "not active")
        {            
            UpdateObject();
        }
    }

    void UpdateObject()
    {
        bool redPlayerNear = playerNearToObj(redPlayerName);
        bool bluePlayerNear = playerNearToObj(bluePlayerName);
        bool playersInContact = GameStateManager.Instance.playersInContact;

        if (catcherPlayer == null) //ningú porta l'objecte
        {
            if (redPlayerNear || bluePlayerNear) //si algun jugador està aprop d'un objecte
            {
                if (firstTime == -1.0f) firstTime = Time.time;
                else if (playersInContact == false && testCooldown()) catchObject(redPlayerNear, bluePlayerNear);
            }
            else if (firstTime != -1.0f) firstTime = -1.0f;
        }

        else // algú té l'objecte 
        {
            if (ObjectsManager.Instance.bluePlayerObject != null && bluePlayerNear)
            {  // BLUE PLAYER aprop del objecte i porta un altre               
                updateObjectPos(ObjectsManager.Instance.bluePlayerObject, redPlayerNear, bluePlayerNear, playersInContact);
            }
            else if (ObjectsManager.Instance.redPlayerObject != null && redPlayerNear)
            { // RED PLAYER aprop del objecte i porta un altre
                updateObjectPos(ObjectsManager.Instance.redPlayerObject, redPlayerNear, bluePlayerNear, playersInContact);
            }
            else if (firstTime != -1.0f) firstTime = -1.0f; //cap jugador està en posició de agafar un objecte
        }
    }

    void updateObjectPos(ObjectScript playerObject, bool redPlayerNear, bool bluePlayerNear, bool playersInContact)
    {
        if (playerObject.transform == this.transform) //el objecte el té un jugador, actualitzar la seva posició
        {
            this.transform.position = catcherPlayer.transform.position + objectPlayerDisp;
        }
        else if (!playersInContact)  //no es l'objecte que tenim, iniciar el cooldown per fer el intercanvi
        {
            if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
            else if (testCooldown())
            {
                catchObject(redPlayerNear, bluePlayerNear);
            }
        }
    }

    bool playerNearToObj(string playerName)
    {
        GameObject player = GameObject.Find(playerName);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 objPos = new Vector2(this.transform.position.x, this.transform.position.z);
        float dist = Vector2.Distance(playerPos, objPos);

        if (dist < coliderPlayerDist)
        {
            return true;
        }
        
        return false;
    }

    bool testCooldown()
    {
        float timeDif = Time.time - firstTime;
        //Debug.Log("Object cooldown: " + timeDif);
        return (timeDif > cooldown);
    }

    void catchObject(bool redPlayerNear, bool bluePlayerNear)
    {      
        GameObject DroppedObjects = GameObject.Find("DroppedObjects");

        if (redPlayerNear)
        {
            catcherPlayer = GameObject.Find(redPlayerName);

            ObjectsManager.Instance.untagAllObjectsWithTag(redPlayerObjTag);            
            gameObject.tag = redPlayerObjTag;     
            
            if (ObjectsManager.Instance.redPlayerObject != null)
            {
                ObjectsManager.Instance.redPlayerObject.transform.SetParent(DroppedObjects.transform);
                ObjectsManager.Instance.redPlayerObject.catcherPlayer = null;
                ObjectsManager.Instance.redPlayerObject = null; // l'anterior objecte que estava agafant el jugador, ja no el té                                
            }            
        }


        else if (bluePlayerNear)
        {
            catcherPlayer = GameObject.Find(bluePlayerName);

            ObjectsManager.Instance.untagAllObjectsWithTag(bluePlayerObjTag);            
            gameObject.tag = bluePlayerObjTag;

            if (ObjectsManager.Instance.bluePlayerObject != null)
            {
                ObjectsManager.Instance.bluePlayerObject.transform.SetParent(DroppedObjects.transform);
                ObjectsManager.Instance.bluePlayerObject.catcherPlayer = null;
                ObjectsManager.Instance.bluePlayerObject = null; // l'anterior objecte que estava agafant el jugador, ja no el té                
            }            
        }

        firstTime = -1.0f;        
        this.transform.SetParent(DontDestroyEntities.transform);        

        ObjectsManager.Instance.catchObject(catcherPlayer.name, this);

        if (catchSound)
        {
            SoundManager.Instance.Play(catchSoundName);
        }
    }

}
