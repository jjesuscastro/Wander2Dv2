using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object;
using GameManager;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject excNotif;
        public float fadeRate = 0.25f;
        public Interactable focus;
        public DoorFade doorFocus;
        bool isRespawning = false;
        bool isResetting = false;
        bool hasReset = true;
        Vector3 respawnPoint;
        Vector3 resetPoint;
        Color color;
        Rigidbody2D rigidBody2D;

        void Start()
        {
            resetPoint = transform.position;
            respawnPoint = resetPoint;
            color = gameObject.GetComponent<SpriteRenderer>().color;
            rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (isResetting)
            {
                isResetting = false;
                hasReset = true;
            }
            if (isRespawning)
            {
                if (color.a < 1)
                {
                    color.a += fadeRate;
                    gameObject.GetComponent<SpriteRenderer>().color = color;
                }
                else
                {
                    isRespawning = false;
                }
            }
        }

        public void RemoveColor()
        {
            color.a = 0;
        }

        public void WalkIn()
        {
            GetComponent<CharacterController2D>().EnableColliders();

            color.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            isRespawning = true;
        }

        public void reset()
        {
            hasReset = false;
            isResetting = true;
            color.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            isRespawning = true;
            transform.position = resetPoint;
        }

        public void respawn()
        {
            color.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            isRespawning = true;
            transform.position = respawnPoint;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(other.transform);
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("MovingPlatform") && rigidBody2D.simulated)
            {
                transform.parent = null;
            }
        }
        
        void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Checkpoint"))
            {
                respawnPoint = other.transform.position;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            DoorFade doorFade = other.gameObject.GetComponent<DoorFade>();
            FallEvent fallEvent = other.gameObject.GetComponent<FallEvent>();

            if(doorFade != null)
            {
                if(gameObject.CompareTag("Player") && doorFade.mcAllowed || gameObject.CompareTag("NPC") && doorFade.npcAllowed)
                {
                    excNotif.SetActive(true);
                    SetDoorFocus(doorFade);
                }
            }

            if (interactable != null)
            {
                if(!(interactable is DialogueTrigger))
                    excNotif.SetActive(true);
                SetFocus(interactable);
            }

            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(other.transform);
                Debug.Log("[PlayerController.cs] - MovingPlatform. New parent: " + other.name);
            }

            if (other.gameObject.CompareTag("Checkpoint"))
            {
                respawnPoint = other.transform.position;
                Debug.Log("[PlayerController.cs] - Checkpoint. New checkpoint: " + respawnPoint);
            }

            if (other.gameObject.CompareTag("FallDetector"))
            {
                if(fallEvent != null)
                    fallEvent.fallTrigger.Invoke();
                    
                PlayerMentalHealth.instance.changeHealth(-0.05f);
                if(!isResetting && hasReset)
                    respawn();

                Debug.Log("[PlayerController.cs] - FallDetector encountered.");
            }

            if (other.gameObject.CompareTag("EndScene"))
            {
                SceneController.instance.LoadNewScene();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (focus != null && other != null)
            {
                if (focus.gameObject == other.gameObject)
                {
                    excNotif.SetActive(false);
                    RemoveFocus();
                }
            }

            if(doorFocus != null && other != null)
            {
                if (doorFocus.gameObject == other.gameObject)
                {
                    excNotif.SetActive(false);
                    RemoveDoorFocus();
                }
            }

            if (other.gameObject.CompareTag("MovingPlatform") && rigidBody2D.simulated)
            {
                transform.parent = null;
            }
        }

        public void SetFocus(Interactable newFocus)
        {
            focus = newFocus;
            newFocus.OnFocus(transform);
        }

        public void SetDoorFocus(DoorFade newFocus)
        {
            doorFocus = newFocus;
            newFocus.OnFocus(transform);
        }

        public void RemoveFocus()
        {
            focus.OnDefocus();
            focus = null;
        }

        public void RemoveDoorFocus()
        {
            doorFocus.OnDefocus();
            doorFocus = null;
        }
    }
}