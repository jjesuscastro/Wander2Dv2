using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Object
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class UseLocation : Interactable
    {
        [SerializeField] Item item;
        public UnityEvent onUse;

        // Update is called once per frame
        void Update()
        {
            if (item.isUsed)
            {
                if(onUse != null)
                    onUse.Invoke();
                Destroy(this.gameObject);
            }
            radius = GetComponent<CircleCollider2D>().radius;
        }
    }
}