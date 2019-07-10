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
        [SerializeField]
        private PlayerSwitch playerSwitch;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>

        public override bool Interact()
        {
            // Debug.Log("NPC Available");
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
            playerSwitch.ObtainedNPC();
            NPC.SetActive(true);
            Destroy(gameObject);
        }
    }
}
