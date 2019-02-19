using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Object
{
    public class EntryPickup : Interactable
    {
        public Entry entry;
        
        public override void Interact()
        {
            Pickup();
        }

        void Pickup()
        {
            Debug.Log("Picking up journal entry " + entry.name);
            bool wasPickedUp = Journal.instance.Add(entry);
            
            if(wasPickedUp)
                Destroy(gameObject);
        }
    }
}
