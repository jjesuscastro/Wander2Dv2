using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    [RequireComponent (typeof(CircleCollider2D))]
    public class Interactable : MonoBehaviour
    {

        public float radius = 3;
        bool isFocused = false;
        bool hasInteracted = false;
        Transform player;

        public virtual void Interact()
        {
            Debug.Log("Interacted with " + gameObject.name);
        }

        // Update is called once per frame
        void Update()
        {
            radius = GetComponent<CircleCollider2D>().radius;

            if(isFocused && player != null && hasInteracted != true)
            {
                float distance = Vector3.Distance(player.position, transform.position);
                if(distance <= radius)
                {
                    Interact();
                    hasInteracted = true;
                }
            }
        }

        public void OnFocus(Transform playerTransform)
        {
            isFocused = true;
            player = playerTransform;
        }

        public void OnDefocus()
        {
            isFocused = false;
            player = null;
            hasInteracted = false;
        }


        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
