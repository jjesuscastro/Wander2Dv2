using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using Player;

namespace Object
{
    public class NPCInteract : Interactable
    {
        public GameObject NPC;
        [SerializeField]
        private PlayerSwitch playerSwitch;
        
        public override bool Interact()
        {
            if(Input.GetButtonDown("Interact"))
            {
                Talk();
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
