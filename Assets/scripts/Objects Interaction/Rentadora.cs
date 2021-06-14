using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rentadora : MonoBehaviour
{
    public float colliderPlayerDist = 10.0f;
    public float cooldown = 1.5f;
    public string robaName = "suit";    

    private string redPlayerName = "Red Player";
    private string bluePlayerName = "Blue Player";
    private float firstTime;

    [HideInInspector]
    public bool focCuinaActive;

    //cuina
    public string cuinaName = "cuina";
    private GameObject cuinaContainer;       

    //roba (traje)
    public string containerName; //roba
    private GameObject robaContainer;

    //tarjeta
    public string tarjetaName;
    private GameObject tarjetaContainer;

    //rentadora 
    public string rentadoraName;
    private GameObject rentadoraContainer;

    //rentadora sound
    public string rentadoraStartSound = "rentadoraStart";
    public string rentadoraEndSound = "rentadoraEnd";

    //tarjeta appear sound
    public string tarjetaAppearSoundName = "tarjetaAppear";
    private bool endSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = -1.0f;
        bool endSoundPlayed = false;

        robaContainer = GameObject.Find(containerName);
        cuinaContainer = GameObject.Find(cuinaName);
        tarjetaContainer = GameObject.Find(tarjetaName);
        rentadoraContainer = GameObject.Find(rentadoraName);

        checkFocCuinaApagat();

        tarjetaContainer.active = false;
        if(robaContainer.tag == "not active")
        {
            if (focCuinaActive == false)
            {
                tarjetaContainer.active = true;
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {        
        UpdateRentadora();

        if(robaContainer.tag == "not active") //roba posada a la rentadora
        {
            checkFocCuinaApagat();
            if(focCuinaActive == false && !endSoundPlayed)
            {
                rentadoraContainer.tag = "not active";
                Debug.Log("S'HA TROBAT LA TARJETA, rentadora acabada");
                tarjetaContainer.active = true;

                if (!endSoundPlayed)
                {                   
                    SoundManager.Instance.Play2(rentadoraEndSound, 2);
                    SoundManager.Instance.Play2(tarjetaAppearSoundName, 7);
                    endSoundPlayed = true;
                }                
            }            
        }

        if(rentadoraContainer.tag == "active") //rentadora acabada
        {
            tarjetaContainer.active = false;
        }
    }

    void checkFocCuinaApagat()
    {
        if(cuinaContainer.tag == "active")
        {
            focCuinaActive = true;
        }
        else if(cuinaContainer.tag == "not active")
        {
            focCuinaActive = false;
        }
    }

    void UpdateRentadora()
    {
        bool redPlayerNear = playerNearToObj(redPlayerName);
        bool bluePlayerNear = playerNearToObj(bluePlayerName);

        if (redPlayerNear || bluePlayerNear) //si algun jugador està aprop d'un objecte
        {
            if (redPlayerNear && ObjectsManager.Instance.redPlayerObject != null) //vermell aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.redPlayerObject.gameObject.name == robaName) //vermell aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("RED PLAYER HA POSAT LA RENTADORA");
                        interact();
                    }                   
                }
            }
            else if (bluePlayerNear && ObjectsManager.Instance.bluePlayerObject != null) //blau aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.bluePlayerObject.gameObject.name == robaName) //blau aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("BLUE PLAYER HA POSAT LA RENTADORA");
                        interact();
                    }                    
                }
            }            
        }
        else if (firstTime != -1.0f) firstTime = -1.0f; //cap jugador està en posició de agafar un objecte
    }

    void interact()
    {                
        robaContainer.tag = "not active";        

        GameObject DroppedObjects = GameObject.Find("DroppedObjects");

        if (ObjectsManager.Instance.redPlayerObject != null && ObjectsManager.Instance.redPlayerObject.gameObject.name == robaName)
        {
            ObjectsManager.Instance.redPlayerObject.gameObject.active = false;
            ObjectsManager.Instance.redPlayerObject.transform.SetParent(DroppedObjects.transform);
            ObjectsManager.Instance.redPlayerObject.catcherPlayer = null;
            ObjectsManager.Instance.redPlayerObject = null; // l'anterior objecte que estava agafant el jugador, ja no el té  
        }
        else if (ObjectsManager.Instance.bluePlayerObject != null && ObjectsManager.Instance.bluePlayerObject.gameObject.name == robaName)
        {
            ObjectsManager.Instance.bluePlayerObject.gameObject.active = false;
            ObjectsManager.Instance.bluePlayerObject.transform.SetParent(DroppedObjects.transform);
            ObjectsManager.Instance.bluePlayerObject.catcherPlayer = null;
            ObjectsManager.Instance.bluePlayerObject = null; // l'anterior objecte que estava agafant el jugador, ja no el té
        }

        SoundManager.Instance.Play(rentadoraStartSound);

    }

    bool playerNearToObj(string playerName)
    {
        GameObject player = GameObject.Find(playerName);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 objPos = new Vector2(this.transform.position.x, this.transform.position.z);
        float dist = Vector2.Distance(playerPos, objPos);
        if (dist < colliderPlayerDist) return true;
        return false;
    }

    bool testCooldown()
    {
        float timeDif = Time.time - firstTime;
        //Debug.Log(timeDif);
        return (timeDif > cooldown);
    }

}
