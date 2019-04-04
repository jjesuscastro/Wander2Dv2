using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Object
{
    public class UseLocation : Interactable
    {
        [SerializeField] private Item requiredItem;
        [SerializeField] private GameObject objectToEnable;

        public override bool Interact()
        {
            if(requiredItem.isUsed)
            {
                Inventory.instance.Remove(requiredItem);
                EnableComponent();
            
                return true;
            }

            return false;
        }

        void EnableComponent(){
            objectToEnable.SetActive(true);
        }
    }
}