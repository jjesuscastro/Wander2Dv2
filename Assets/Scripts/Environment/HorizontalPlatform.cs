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
    PlayerMovement mc;
    PlayerMovement npc;
    public bool disableMovements = false;
    public float maxX;
    public float minX;
    public float speed;
    public bool moveRight = true;
    public bool stop = true;
    public bool playerOnPlatform = false;
    public bool NPCOnPlatform = false;
    bool oneDirection = true;
    bool rightEnd = false;
    float acceleration = 0;
    bool mcEnabled  = false;
    bool npcEnabled = false;

    void Start()
    {
        mc = MC.instance.gameObject.GetComponent<PlayerMovement>();
        npc = NPC.instance.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > minX + 0.5f && playerOnPlatform && NPCOnPlatform && transform.Find("wall").gameObject != null)
        {
            transform.Find("wall").gameObject.SetActive(true);
        }

        if (!stop)
        {
            if (transform.localPosition.x >= maxX - 0.5)
            {
                acceleration = 0;
                moveRight = false;
                rightEnd = true;

                if(onStop != null)
                    onStop.Invoke();

                if(disableMovements)
                    EnableMovements();
            }
            
            if (transform.localPosition.x <= minX)
            {
                acceleration = 0;
                moveRight = true;
                    
                if(disableMovements)
                    EnableMovements();
            }

            if(acceleration < 1)
                acceleration += 0.01f;

            if (moveRight)
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * acceleration, transform.position.y);
            else if (!moveRight && !oneDirection)
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime * acceleration, transform.position.y);
        }

        if(Input.GetButtonDown("Interact"))
        {
            if(playerOnPlatform == true && NPCOnPlatform == true)
            {
                stop = false;
                if(disableMovements)
                    StopMovements();
                if(onTakeOff != null)
                    onTakeOff.Invoke();

                Debug.Log("[HorizontalPlatform.cs] - Platform moving. On platform (mc, npc): " + playerOnPlatform + ", " + NPCOnPlatform);
            }

            if(playerOnPlatform && !NPCOnPlatform)
                PopupNotification.instance.ShowPopup("I need to wait for my friend!");

            if(!playerOnPlatform && NPCOnPlatform)
                PopupNotification.instance.ShowPopup("I need to wait for my friend!");
        }
    }

    void EnableMovements()
    {
        if(mcEnabled)
        {
            mc.enabled = true;
            mcEnabled = false;
            Debug.Log("[HorizontalPlatform.cs] - Enabling movements. MC active.");
        } else if (npcEnabled) {
            
            npc.enabled = true;
            npcEnabled = false;
            Debug.Log("[HorizontalPlatform.cs] - Enabling movements. NPC active.");
        }
    }
    
    void StopMovements()
    {
        if(mc == null || npc == null)
        {
            mc = MC.instance.gameObject.GetComponent<PlayerMovement>();
            npc = NPC.instance.gameObject.GetComponent<PlayerMovement>();
        }

        if(mc.isActiveAndEnabled)
        {
            mc.enabled = false;
            mcEnabled = true;
            Debug.Log("[HorizontalPlatform.cs] - Disabling movements. MC active.");
        } else if (npc.isActiveAndEnabled){
            npc.enabled = false;
            npcEnabled = true;
            Debug.Log("[HorizontalPlatform.cs] - Disabling movements. NPC active.");
        }
    }

    public void Reset()
    {
        if(transform.localPosition.x > minX)
        {
            moveRight = false;
            stop = false;
            rightEnd = false;
            acceleration = 0;
        } else
        {
            moveRight = true;
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
