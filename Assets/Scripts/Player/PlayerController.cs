using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float fadeRate = 0.25f;
        public Interactable focus;
        bool isRespawning = false;
        Vector3 respawnPoint;
        Color color;

        void Start()
        {
            respawnPoint = transform.position;
            color = gameObject.GetComponent<SpriteRenderer>().color;
        }

        void Update()
        {
            if(isRespawning)
            {
                if(color.a < 1)
                {
                    color.a += fadeRate;
                    gameObject.GetComponent<SpriteRenderer>().color = color;
                } else {
                    isRespawning = false;
                }
            }
        }

        public void respawn() {
            color.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = color;
            isRespawning = true;
            transform.position = respawnPoint;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if(interactable != null)
            {
                SetFocus(interactable);
            }

            if(other.gameObject.CompareTag("Checkpoint"))
            {
                respawnPoint = other.transform.position;
            }

            if(other.gameObject.CompareTag("FallDetector"))
            {
                PlayerMentalHealth.instance.changeHealth(-0.05f);
                respawn();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if(focus != null && other != null)
            {
                if(focus.gameObject == other.gameObject)
                {
                    RemoveFocus();
                }
            }
        }

        public void SetFocus(Interactable newFocus) 
        {
            focus = newFocus;
            newFocus.OnFocus(transform);
        }

        public void RemoveFocus()
        {
            focus.OnDefocus();
            focus = null;
        }
    }
}