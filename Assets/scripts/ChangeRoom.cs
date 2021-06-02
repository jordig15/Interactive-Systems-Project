using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRoom : MonoBehaviour
{
    public string nextSceneName;
    public GameObject redPlayer;
    public GameObject bluePlayer;
    public GameObject collider;

    public float colliderPlayersDist = 15.0f;

    public float cooldown = 1.5f;    

    float firstTime;
    
    bool playersInContact;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Vector3.Distance(collider.transform.position, redPlayer.transform.position) < colliderPlayersDist) return true;
        return false;
    }

    bool testCooldown()
    {
        float timeDif = Time.time - firstTime;
        Debug.Log(timeDif);
        return (timeDif > cooldown);
    }

    void changeRoom()
    {
        Debug.Log("Escena canviada!");
        firstTime = -1.0f;
        SceneManager.LoadScene(nextSceneName);        
    }
}
