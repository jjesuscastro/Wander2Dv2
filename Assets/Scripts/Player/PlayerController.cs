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
        Vector3 respawnPoint;
        Color color;
        Rigidbody2D rigidBody2D;

        void Start()
        {
            respawnPoint = transform.position;
            color = gameObject.GetComponent<SpriteRenderer>().color;
            rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
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

        public void respawn()
        {
            color.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            isRespawning = true;
            transform.position = respawnPoint;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            DoorFade doorFade = other.gameObject.GetComponent<DoorFade>();

            if(doorFade != null)
            {
                excNotif.SetActive(true);
                SetDoorFocus(doorFade);
            }

            if (interactable != null)
            {
                excNotif.SetActive(true);
                SetFocus(interactable);
            }

            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(other.transform);
            }

            if (other.gameObject.CompareTag("Checkpoint"))
            {
                respawnPoint = other.transform.position;
            }

            if (other.gameObject.CompareTag("FallDetector"))
            {
                PlayerMentalHealth.instance.changeHealth(-0.05f);
                respawn();
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