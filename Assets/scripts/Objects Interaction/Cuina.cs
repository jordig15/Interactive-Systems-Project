using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuina : MonoBehaviour
{
    public float colliderPlayerDist = 10.0f;
    public float cooldown = 1.5f;
    public string extintorName = "Fire Extinguisher";
    public string containerName;

    private string redPlayerName = "Red Player";
    private string bluePlayerName = "Blue Player";
    private float firstTime;

    private GameObject focGameObject;
    private GameObject container;

    private bool focActive;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = -1.0f;                

        focGameObject = GameObject.Find("fire");
        container = GameObject.Find(containerName);        
        if(container.tag == "active")
        {
            focActive = true;
            focGameObject.active = true;
        }
        else if(container.tag == "not active")
        {
            focActive = false;
            focGameObject.active = false; //posar inactiu el foc al joc
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (focActive) UpdateLlarDeFoc();
    }

    void UpdateLlarDeFoc()
    {
        bool redPlayerNear = playerNearToObj(redPlayerName);
        bool bluePlayerNear = playerNearToObj(bluePlayerName);

        if (redPlayerNear || bluePlayerNear) //si algun jugador està aprop d'un objecte
        {
            if (redPlayerNear && ObjectsManager.Instance.redPlayerObject != null) //vermell aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.redPlayerObject.gameObject.name == extintorName) //vermell aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("RED PLAYER APAGA FOC");
                        interact();
                    }                   
                }
            }
            else if (bluePlayerNear && ObjectsManager.Instance.bluePlayerObject != null) //blau aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.bluePlayerObject.gameObject.name == extintorName) //blau aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("BLUE PLAYER APAGA FOC");
                        interact();
                    }                    
                }
            }            
        }
        else if (firstTime != -1.0f) firstTime = -1.0f; //cap jugador està en posició de agafar un objecte
    }

    void interact()
    {
        focGameObject.active = false; //posar inactiu el foc al joc
        focActive = false;
        container.tag = "not active";
        
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
        return (timeDif > cooldown);
    }

}
