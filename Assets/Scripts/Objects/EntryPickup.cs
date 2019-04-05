using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UI;

namespace Object
{
    public class EntryPickup : Interactable
    {
        public Entry entry;
        
        public override bool Interact()
        {
            Pickup();
            return true;
        }

        void Pickup()
        {
            Debug.Log("Picking up journal entry " + entry.name);
            bool wasPickedUp = Journal.instance.Add(entry);
            
            if(wasPickedUp)
            {
                Destroy(gameObject);
                PopupNotification.instance.ShowPopup("Journal entry picked up!");
            }
        }
    }
}
