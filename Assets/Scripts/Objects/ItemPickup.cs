using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Object
{
    public class ItemPickup : Interactable
    {
        public Item item;

        public override bool Interact()
        {
            Pickup();
            return true;
        }

        void Pickup()
        {
            Debug.Log("Picking up item " + item.name);
            bool wasPickedUp = Inventory.instance.Add(item);
            
            if(wasPickedUp)
                Destroy(gameObject);
        }
    }
}
