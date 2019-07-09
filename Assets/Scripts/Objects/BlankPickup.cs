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

        public override bool Interact()
        {
            base.Interact();
            Pickup();
            return true;
        }

        void Pickup()
        {
            Debug.Log("Picking up blank pickup " + gameObject.name);

            if(!doNotDestroy)
                Destroy(gameObject);
            PopupNotification.instance.ShowPopup(popupMessage);
        }
    }
}
