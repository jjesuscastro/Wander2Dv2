using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UI;
using Player;

public class HorizontalPlatform : MonoBehaviour
{
    public UnityEvent onTakeOff;
    public UnityEvent onStop;
    public PlayerMovement mc;
    public PlayerMovement npc;
    public bool disableMovements = false;
    public float maxX;
    public float minX;
    public float speed;
    public bool moveRight = true;
    public bool oneDirection = false;
    public bool stop = true;
    public bool playerOnPlatform = false;
    public bool NPCOnPlatform = false;
    bool rightEnd = false;
    float acceleration = 0;
    public string NPCName;
    bool mcEnabled  = false;
    bool npcEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if(playerOnPlatform && NPCOnPlatform && transform.Find("wall").gameObject != null)
        {
            transform.Find("wall").gameObject.SetActive(true);
        }

        if (!stop)
        {
            if (transform.localPosition.x >= maxX)
            {
                acceleration = 0;
                moveRight = false;
                rightEnd = true;
                if (oneDirection)
                    stop = true;

                if(onStop != null)
                    onStop.Invoke();

                if(disableMovements)
                    EnableMovements();
            }
            
            if (transform.localPosition.x <= minX)
            {
                acceleration = 0;
                moveRight = true;
                if (oneDirection)
                    stop = true;
                    
                if(disableMovements)
                    EnableMovements();
            }

            if(acceleration < 1)
                acceleration += 0.01f;

            if (moveRight)
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * acceleration, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime * acceleration, transform.position.y);
        }

        if(Input.GetButtonDown("Interact") && !rightEnd)
        {
            if(playerOnPlatform == true && NPCOnPlatform == true)
            {
                stop = false;
                if(disableMovements)
                    StopMovements();
                if(onTakeOff != null)
                    onTakeOff.Invoke();
            }

            if(playerOnPlatform && !NPCOnPlatform)
                PopupNotification.instance.ShowPopup(NPCName + "I need to wait for my friend!");

            if(!playerOnPlatform && NPCOnPlatform)
                PopupNotification.instance.ShowPopup("I need to wait for my friend!");
        }
    }

    void EnableMovements()
    {
        if(mcEnabled)
        {
            mcEnabled = false;
            mc.enabled = true;
        } else if (npcEnabled) {
            npcEnabled = false;
            npc.enabled = true;
        }
    }
    
    void StopMovements()
    {
        if(mc == null || npc == null)
        {
            PlayerMovement[] players = GameObject.FindObjectsOfType<PlayerMovement>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].gameObject.CompareTag("Player"))
                    mc = players[i];

                if (players[i].gameObject.CompareTag("NPC"))
                    npc = players[i];
            }
        }

        if(mc.isActiveAndEnabled)
        {
            mcEnabled = true;
            mc.enabled = false;
        } else if (npc.isActiveAndEnabled){
            npcEnabled = true;
            npc.enabled = false;
        }
    }

    public void Reset()
    {
        if(transform.localPosition.x > minX)
        {
            moveRight = false;
            oneDirection = true;
            stop = false;
            rightEnd = false;
            acceleration = 0;
        } else
        {
            moveRight = true;
            oneDirection = true;
            stop = false;
            acceleration = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerOnPlatform = true;

        if (other.CompareTag("NPC"))
            NPCOnPlatform = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = true;

        if (other.gameObject.CompareTag("NPC"))
            NPCOnPlatform = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerOnPlatform = false;

        if (other.CompareTag("NPC"))
            NPCOnPlatform = false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = false;

        if (other.gameObject.CompareTag("NPC"))
            NPCOnPlatform = false;
    }
}
