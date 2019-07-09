using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class HorizontalPlatform : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float speed;
    public bool moveRight = true;
    public bool oneDirection = false;
    public bool stop = true;
    bool playerOnPlatform = false;
    bool NPCOnPlatform = false;
    bool rightEnd = false;
    public string NPCName;

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if (transform.localPosition.x >= maxX)
            {
                moveRight = false;
                rightEnd = true;
                if (oneDirection)
                    stop = true;
            }
            
            if (transform.localPosition.x <= minX)
            {
                moveRight = true;
                if (oneDirection)
                    stop = true;
            }

            if (moveRight)
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }

        if(Input.GetButtonDown("Interact") && !rightEnd)
        {
            if(playerOnPlatform && NPCOnPlatform)
                stop = false;

            if(playerOnPlatform && !NPCOnPlatform)
                PopupNotification.instance.ShowPopup(NPCName + " is not on yet.");

            if(!playerOnPlatform && NPCOnPlatform)
                PopupNotification.instance.ShowPopup("Ziv is not on yet.");
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
        } else
        {
            moveRight = true;
            oneDirection = true;
            stop = false;
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
