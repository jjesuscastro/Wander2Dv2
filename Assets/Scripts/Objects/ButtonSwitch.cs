using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Object
{
    public class ButtonSwitch : Interactable
    {
        public UnityEvent onInteract;
        public override bool Interact()
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (onInteract != null)
                    onInteract.Invoke();

                return true;
            }

            return false;
        }
    }
}