using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySound : MonoBehaviour
{
    private GameObject redPlayer;
    private GameObject bluePlayer;

    public float soundDisplayProximity = 10.0f;
    public string soundName;


    public bool collisionSound = false;
    [HideInInspector]
    public bool canDisplay;
    public string sound1CollisionName;
    public string sound2CollisionName = "";

    // Start is called before the first frame update
    void Start()
    {
        redPlayer = GameObject.Find("Red Player");
        bluePlayer = GameObject.Find("Blue Player");
        canDisplay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionSound)
        {
            if (sound1CollisionName != "" && SoundManager.Instance.IsPlaying(sound1CollisionName)) canDisplay = false;
            else if (sound2CollisionName != "" && SoundManager.Instance.IsPlaying(sound2CollisionName)) canDisplay = false;
            else canDisplay = true;
        }       

        if(this.gameObject.active && (playerNearToObj(redPlayer) || playerNearToObj(bluePlayer)))
        {
            if (canDisplay && !SoundManager.Instance.IsPlaying(soundName))
            {
                SoundManager.Instance.Play(soundName);
            }           
        }
    }

    bool playerNearToObj(GameObject player)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 objPos = new Vector2(this.transform.position.x, this.transform.position.z);
        float dist = Vector2.Distance(playerPos, objPos);
        if (dist < soundDisplayProximity) return true;
        return false;
    }
}
