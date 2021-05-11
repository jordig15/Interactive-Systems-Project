using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeRoom : MonoBehaviour
{
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider");
        //bool playersInContact = GameStateManager.Instance.playersInContact;
        bool playersInContact = true;
        if ((other.CompareTag("Player1") || other.CompareTag("Player2")) && playersInContact)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
