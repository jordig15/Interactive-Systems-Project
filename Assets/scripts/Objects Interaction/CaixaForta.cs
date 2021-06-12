using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaForta : MonoBehaviour
{
    public float colliderPlayerDist = 10.0f;
    public float cooldown = 1.5f;

    public string tarjetaName = "tarjeta";
    private GameObject tarjetaContainer;

    public string containerName;

    //sound
    public string soundName = "caixaForta";

    //clau 
    public string clauFinalName;
    private GameObject clauFinalContainer;

    private string redPlayerName = "Red Player";
    private string bluePlayerName = "Blue Player";
    private float firstTime;

    //sound wrong
    public string caixaFortaWrongSoundName = "wrongPass";

    private GameObject caixaFortaContainer;

    private bool caixaFortaActive;
    // Start is called before the first frame update
    void Start()
    {
        firstTime = -1.0f;                

        caixaFortaContainer = GameObject.Find(containerName);
        tarjetaContainer = GameObject.Find(tarjetaName);
        clauFinalContainer = GameObject.Find(clauFinalName);

        if (caixaFortaContainer.tag == "active")
        {
            clauFinalContainer.active = false;
            caixaFortaActive = true;            
        }
        else if(caixaFortaContainer.tag == "not active")
        {
            caixaFortaActive = false;
            clauFinalContainer.active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (caixaFortaActive) UpdateCaixaForta();
    }

    void UpdateCaixaForta()
    {
        bool redPlayerNear = playerNearToObj(redPlayerName);
        bool bluePlayerNear = playerNearToObj(bluePlayerName);

        if (redPlayerNear || bluePlayerNear) //si algun jugador està aprop d'un objecte
        {
            if (redPlayerNear && ObjectsManager.Instance.redPlayerObject != null) //vermell aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.redPlayerObject.gameObject.name == tarjetaName) //vermell aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("RED PLAYER DESBLOQUEJA LA CAIXA FORTA");
                        interact();
                    }                   
                }
                else
                {
                    wrongPass();
                }
            }
            else if (bluePlayerNear && ObjectsManager.Instance.bluePlayerObject != null) //blau aprop de la llar de foc i porta algun objecte
            {
                if (ObjectsManager.Instance.bluePlayerObject.gameObject.name == tarjetaName) //blau aprop i porta un extintor
                {
                    if (firstTime == -1.0f) firstTime = Time.time; //si no s'ha entrat al cooldown
                    else if (testCooldown())
                    {
                        Debug.Log("BLUE PLAYER DESBLOQUEJA LA CAIXA FORTA");
                        interact();
                    }                    
                }
                else
                {
                    wrongPass();
                }
            }
            else
            {
                wrongPass();
            }
        }
        else if (firstTime != -1.0f) firstTime = -1.0f; //cap jugador està en posició de agafar un objecte
    }

    void wrongPass()
    {
        Debug.Log("merda");
        if (!SoundManager.Instance.IsPlaying(caixaFortaWrongSoundName))
        {
            SoundManager.Instance.Play(caixaFortaWrongSoundName);
        }
    }

    void interact()
    {
        if (soundName != null)
        {
            SoundManager.Instance.Play(soundName);
        }
        
        //container.CaixaFortaSound.canDisplay = false;
    
        clauFinalContainer.active = true;
        caixaFortaActive = false;
        caixaFortaContainer.tag = "not active";
        tarjetaContainer.active = false;
        
    }

    bool playerNearToObj(string playerName)
    {
        GameObject player = GameObject.Find(playerName);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 objPos = new Vector2(this.transform.position.x, this.transform.position.z);
        float dist = Vector2.Distance(playerPos, objPos);
        if (dist < colliderPlayerDist) return true;
        return false;
    }

    bool testCooldown()
    {
        float timeDif = Time.time - firstTime;
        Debug.Log(timeDif);
        return (timeDif > cooldown);
    }

}
