using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameManager;
using Player;

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
            
            playerSwitch.ObtainedNPC();
            NPC.SetActive(true);
            Destroy(gameObject);
        }
    }
}
