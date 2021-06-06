using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRoom : MonoBehaviour
{
    public string nextSceneName;    

    private GameObject redPlayer;
    private GameObject bluePlayer;    

    private float cooldown = 1.0f;
    private float colliderPlayersDist = 20.0f;

    private float firstTime;    
    private bool playersInContact;

    // Start is called before the first frame update
    void Start()
    {
        redPlayer = GameObject.Find("Red Player");
        bluePlayer = GameObject.Find("Blue Player");

        firstTime = -1.0f;        
    }

    // Update is called once per frame
    void Update()
    {
        bool playersInContact = GameStateManager.Instance.playersInContact;

        if (playersInContact && playerNearToDoor())
        {        
            if(firstTime == -1.0f) firstTime = Time.time; 
            else if (testCooldown()) changeRoom();            
        }

        else if(firstTime != -1.0f) firstTime = -1.0f;
    }

    bool playerNearToDoor()
    {
        if (Vector3.Distance(GetComponent<Collider>().transform.position, redPlayer.transform.position) < colliderPlayersDist) return true;
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
