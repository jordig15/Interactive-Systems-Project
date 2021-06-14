using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance; //instance

    //players in contact
    private GameObject redPlayer;
    private GameObject bluePlayer;
    private GameObject DontDestroyEntities;
    private float playersDistanceThresh;

    [HideInInspector]
    public bool playersInContact;    

    void Awake()
    {
        Instance = this;

        this.playersInContact = false;
        redPlayer = GameObject.Find("Red Player");
        bluePlayer = GameObject.Find("Blue Player");
        DontDestroyEntities = GameObject.Find("DontDestroyEntities");

        playersDistanceThresh = 10;
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    void Update()
    {
        updatePlayersInContact();
    }

    public void updatePlayersInContact()
    {
        Vector3 redPlayerPos = this.redPlayer.transform.position;
        Vector3 bluePlayerPos = this.bluePlayer.transform.position;
        float dist= Vector3.Distance(redPlayerPos, bluePlayerPos);
            

        playersInContact = false;

        if (dist <= playersDistanceThresh) playersInContact = true;

    }    
}
