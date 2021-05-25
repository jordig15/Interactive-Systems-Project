using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRoom : MonoBehaviour
{
    public string nextSceneName;
    
    public float cooldown = 1.5f;
    float firstTime;
    bool playersInContact;
    bool coliderActivated;

    // Start is called before the first frame update
    void Start()
    {
        coliderActivated = false;
        firstTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (coliderActivated)
        {
            if (testCooldown())
            {
                changeRoom();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        playersInContact = GameStateManager.Instance.playersInContact;
        Debug.Log(playersInContact);

        if ((other.CompareTag("Player1") || other.CompareTag("Player2")) && playersInContact)
        {
            if (firstTime == 0)
            {
                Debug.Log("first time");
                firstTime = Time.time;
                coliderActivated = true;
            }
        }

    }

    bool testCooldown()
    {
        float timeDif = Time.time - firstTime;
        Debug.Log(timeDif);
        return (timeDif > cooldown) && (playersInContact);
    }

    void changeRoom()
    {
        Debug.Log("Escena canviada!");
        firstTime = 0;
        coliderActivated = false;

        SceneManager.LoadScene(nextSceneName);

        
    }
}
