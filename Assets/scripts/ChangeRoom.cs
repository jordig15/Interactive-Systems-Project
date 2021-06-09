using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRoom : MonoBehaviour
{
    public string nextSceneName;

    //portes amb clau
    public bool keyRequired = false;
    private string doorLockedSoundName = "doorLocked";

    public string keyContainerName;
    private GameObject container;

    private GameObject redPlayer;
    private GameObject bluePlayer;    

    private float cooldown = 1.0f;
    private float colliderPlayersDist = 8.0f;

    private float firstTime;    
    private bool playersInContact;

    // Start is called before the first frame update
    void Start()
    {
        redPlayer = GameObject.Find("Red Player");
        bluePlayer = GameObject.Find("Blue Player");

        container = GameObject.Find(keyContainerName);

        firstTime = -1.0f;        
    }

    // Update is called once per frame
    void Update()
    {
        bool playersInContact = GameStateManager.Instance.playersInContact;

        if (playersInContact && playerNearToDoor())
        {
            if (!keyRequired || (keyRequired && isDoorOpened()))
            {
                if (firstTime == -1.0f) firstTime = Time.time;
                else if (testCooldown()) changeRoom();
            }
            else if(keyRequired && !isDoorOpened()) //door closed
            {
                if (!SoundManager.Instance.IsPlaying(doorLockedSoundName))
                {
                    SoundManager.Instance.Play(doorLockedSoundName);
                }               
            }
                       
        }

        else if(firstTime != -1.0f) firstTime = -1.0f;
    }

    bool isDoorOpened()
    {
        
        if (container.tag == "active")
        {
            return false;
        }
        else if (container.tag == "not active")
        {
            return true;
        }
        

        Debug.Log("ERROR isDoorOpened()");
        return true;
    }

    bool playerNearToDoor()
    {        
        Vector2 playerPos = new Vector2(redPlayer.transform.position.x, redPlayer.transform.position.z);
        Vector2 objPos = new Vector2(GetComponent<Collider>().transform.position.x, GetComponent<Collider>().transform.position.z);
        float dist = Vector2.Distance(playerPos, objPos);
        if (dist < colliderPlayersDist) return true;
        return false;
    }

    bool testCooldown()
    {
        float timeDif = Time.time - firstTime;
        //Debug.Log(timeDif);
        return (timeDif > cooldown);
    }

    void changeRoom()
    {
        Debug.Log("Escena canviada!");
        firstTime = -1.0f;
        
        SceneManager.LoadScene(nextSceneName);                
    }
}
