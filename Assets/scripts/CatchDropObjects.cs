using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchDropObjects : MonoBehaviour
{
    public GameObject redPlayer;
    public GameObject bluePlayer;

    public Vector3 objectPlayerDisp = new Vector3(5.0f, 0.0f, 5.0f);
    public float cooldown = 1.5f;
    public float coliderPlayerDist = 15.0f;

    GameObject catcherPlayer;
    float firstTime;
    bool catchedObject;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = -1.0f;
        catchedObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (catchedObject)
        {
            this.transform.position = catcherPlayer.transform.position + objectPlayerDisp;
        }

        else
        {
            bool redPlayerNear = playerNearToObj(redPlayer);
            bool bluePlayerNear = playerNearToObj(bluePlayer);
            //Debug.Log(redPlayerNear + " | " + bluePlayerNear);
            if (redPlayerNear || bluePlayerNear)
            {
                if (firstTime == -1.0f) firstTime = Time.time;
                else if (testCooldown()) catchObject(redPlayerNear, bluePlayerNear);
            }
            else if (firstTime != -1.0f) firstTime = -1.0f;
        }
    }

    bool playerNearToObj(GameObject player)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 objPos = new Vector2(this.transform.position.x, this.transform.position.z);
        float dist = Vector2.Distance(playerPos, objPos);
        if (dist < coliderPlayerDist) return true;
        //Debug.Log(dist);
        return false;
    }

    bool testCooldown()
    {
        float timeDif = Time.time - firstTime;
        Debug.Log(timeDif);
        return (timeDif > cooldown);
    }

    void catchObject(bool redPlayerNear, bool bluePlayerNear)
    {
        catchedObject = true;
        if (redPlayerNear) catcherPlayer = redPlayer;
        else if (bluePlayerNear) catcherPlayer = bluePlayer;
        firstTime = -1.0f;

        Debug.Log(redPlayerNear + " | " + bluePlayerNear);
        Debug.Log("CATCHED OBJECT!");
    }
}


