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

        public override bool Interact()
        {
            TriggerDialogue();

            return true;
        }

        void TriggerDialogue()
        {
            dialogue.TriggerDialogue();
            Destroy(gameObject);
        }
    }
}

