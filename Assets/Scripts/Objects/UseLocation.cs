using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class UseLocation : Interactable
    {
        [SerializeField] Item item;
        [SerializeField] GameObject gameObjectToEnable;

        // Update is called once per frame
        void Update()
        {
            if (item.isUsed)
            {
                EnableGameObject();
                Destroy(this.gameObject);
            }
            radius = GetComponent<CircleCollider2D>().radius;
        }

        void EnableGameObject()
        {
            gameObjectToEnable.SetActive(true);
        }
    }
}