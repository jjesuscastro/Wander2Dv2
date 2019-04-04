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

        public virtual bool Interact()
        {
            Debug.Log("Interacted with " + gameObject.name);
            return false;
        }

        // Update is called once per frame
        protected void Update()
        {
            radius = GetComponent<CircleCollider2D>().radius;

            if(isFocused && player != null && hasInteracted != true)
            {
                float distance = Vector3.Distance(player.position, transform.position);
                if(distance <= radius)
                {
                    hasInteracted = Interact();
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
