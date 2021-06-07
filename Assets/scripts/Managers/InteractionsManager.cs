using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsManager : MonoBehaviour
{
    public static InteractionsManager Instance; //instance

    [HideInInspector]
    public bool llarDeFoc1Active = true;

    void Awake()
    {
        Debug.Log("Awake im");
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    void Update()
    {
        //Debug.Log("IM"+ llarDeFoc1Active);
    }

    
}
