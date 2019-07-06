using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Object
{
    public class DialogueTrigger : Interactable
    {
        [SerializeField]
        private Dialogue dialogue;
        public bool doNotDestroy = false;

        public override bool Interact()
        {
            TriggerDialogue();

            return true;
        }

        void TriggerDialogue()
        {
            dialogue.TriggerDialogue();

            if(!doNotDestroy)
                Destroy(gameObject);
        }
    }
}

