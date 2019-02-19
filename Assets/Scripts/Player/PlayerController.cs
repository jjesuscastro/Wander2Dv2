using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Interactable focus;

        public void respawn() {
            Color transparent = gameObject.GetComponent<SpriteRenderer>().color;
            transparent.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = transparent;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if(interactable != null)
            {
                SetFocus(interactable);
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