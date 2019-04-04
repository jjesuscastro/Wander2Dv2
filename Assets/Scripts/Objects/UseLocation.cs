using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    [RequireComponent (typeof(CircleCollider2D))]
    public class UseLocation : MonoBehaviour
    {
        public float radius = 3;
        [SerializeField] Item item;
        [SerializeField] GameObject gameObjectToEnable;

        // Update is called once per frame
        void Update()
        {
            if(item.isUsed)
                EnableGameObject();
            radius = GetComponent<CircleCollider2D>().radius;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        void EnableGameObject()
        {
            gameObjectToEnable.SetActive(true);
        }
    }
}