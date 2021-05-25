using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public GameObject redPlayer;
    public GameObject bluePlayer;
    public float playersDistanceThresh;

    [HideInInspector]
    public bool playersInContact;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.playersInContact = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 redPlayerPos = this.redPlayer.transform.position;
        Vector3 bluePlayerPos = this.bluePlayer.transform.position;
        float dist = Vector3.Distance(redPlayerPos, bluePlayerPos);

        playersInContact = false;
        if (dist <= playersDistanceThresh)
        {
            playersInContact = true;
        }

        //  Debug.Log(playersInContact);
    }
}
