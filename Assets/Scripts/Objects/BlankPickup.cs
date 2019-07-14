using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UI;

namespace Object
{
    public class BlankPickup : Interactable
    {
        public string popupMessage;
        public bool doNotDestroy = false;
        public bool hasPopup = true;

        public override bool Interact()
        {
            Pickup();
            return true;
        }

        void Pickup()
        {
            Debug.Log("[BlankPickup.cs] - Picking up blank pickup. BlankPickup: " + gameObject.name);

            onPickup.Invoke();

            if(!doNotDestroy)
                Destroy(gameObject);

            if(hasPopup)
                PopupNotification.instance.ShowPopup(popupMessage);
        }
    }
}
