using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameManager;
using Player;
using UI;

namespace Object
{
    public class NPCInteract : Interactable
    {
        public UnityEvent onInteract;
        public GameObject NPC;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>

        public override bool Interact()
        {
            if (Input.GetButtonDown("Interact"))
            {
                Talk();

                if (onInteract != null)
                    onInteract.Invoke();

                return true;
            }

            return false;
        }

        void Talk()
        {
            PopupNotification.instance.ShowPopup("Press 'Q' to switch characters!");
            PlayerSwitch.instance.ObtainedNPC();
            NPC.SetActive(true);
            Destroy(gameObject);
        }
    }
}
