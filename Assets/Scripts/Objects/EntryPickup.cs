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
            Debug.Log("[EntryPickup.cs] - Picking up journal entry. EntryPickup: " + entry.name);
            bool wasPickedUp = Journal.instance.Add(entry);

            if (wasPickedUp)
            {
                PopupNotification.instance.ShowPopup("Journal entry picked up!");
                Destroy(gameObject);
                
                if(onPickup != null)
                    onPickup.Invoke();
            }
        }
    }
}
