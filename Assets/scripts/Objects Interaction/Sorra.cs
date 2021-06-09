using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorra : MonoBehaviour
{
    public float colliderPlayerDist = 10.0f;
    public float cooldown = 1.5f;
    public string aspiradoraName = "VacuumCleaner";
    public string containerName;

    //sound
    public string aspiradoraSoundName = "vacuumCleaner";

    private string redPlayerName = "Red Player";
    private string bluePlayerName = "Blue Player";
    private float firstTime;
    
    private GameObject container;

    private bool sorraActive;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = -1.0f;                
        
        container = GameObject.Find(containerName);        
        if(container.tag == "active")
        {
            sorraActive = true;            
        }
        else if(container.tag == "not active")
        {
            sorraActive = false;
            this.gameObject.active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (sorraActive) UpdateSorra();
    }

    void UpdateSorra()
    {
        bool redPlayerNear = playerNearToObj(redPlayerName);
        bool bluePlayerNear = playerNearToObj(bluePlayerName);

        if (redPlayerNear || bluePlayerNear) //si algun jugador està aprop d'un objecte
        {
            if (redPlayerNear && ObjectsManager.Instance.redPlayerObject != null) //vermell aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.redPlayerObject.gameObject.name == aspiradoraName) //vermell aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("RED PLAYER ASPIRA LA SORRA");
                        interact();
                    }                   
                }
            }
            else if (bluePlayerNear && ObjectsManager.Instance.bluePlayerObject != null) //blau aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.bluePlayerObject.gameObject.name == aspiradoraName) //blau aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("BLUE PLAYER ASPIRA LA SORRA");
                        interact();
                    }                    
                }
            }            
        }
        else if (firstTime != -1.0f) firstTime = -1.0f; //cap jugador està en posició de agafar un objecte
    }

    void interact()
    {
        sorraActive = false;
        this.gameObject.active = false;
        container.tag = "not active";

        GameObject DroppedObjects = GameObject.Find("DroppedObjects");        

        if (ObjectsManager.Instance.redPlayerObject.gameObject.name == aspiradoraName)
        {
            ObjectsManager.Instance.redPlayerObject.gameObject.active = false;
            ObjectsManager.Instance.redPlayerObject.transform.SetParent(DroppedObjects.transform);
            ObjectsManager.Instance.redPlayerObject.catcherPlayer = null;
            ObjectsManager.Instance.redPlayerObject = null; // l'anterior objecte que estava agafant el jugador, ja no el té  
        }
        else if (ObjectsManager.Instance.bluePlayerObject.gameObject.name == aspiradoraName)
        {
            ObjectsManager.Instance.bluePlayerObject.gameObject.active = false;
            ObjectsManager.Instance.bluePlayerObject.transform.SetParent(DroppedObjects.transform);
            ObjectsManager.Instance.bluePlayerObject.catcherPlayer = null;
            ObjectsManager.Instance.bluePlayerObject = null; // l'anterior objecte que estava agafant el jugador, ja no el té
        }       
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
        Debug.Log(timeDif);

        if (!SoundManager.Instance.IsPlaying(aspiradoraSoundName))
        {
            SoundManager.Instance.Play(aspiradoraSoundName);    
        }        

        return (timeDif > cooldown);
    }

}
