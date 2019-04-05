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
        
        public override bool Interact()
        {
            if(Input.GetButtonDown("Interact"))
            {
                Talk();

                if(onInteract != null)
                    onInteract.Invoke();

                return true;
            }

            return false;
        }

        void Talk()
        {
            Debug.Log("Talking to NPC " + NPC.name);
            PopupNotification.instance.ShowPopup("Press 'Q' to use Leo!");
            playerSwitch.ObtainedNPC();
            NPC.SetActive(true);
            Destroy(gameObject);
        }
    }
}
