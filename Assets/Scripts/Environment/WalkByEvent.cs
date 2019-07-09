using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class WalkByEvent : MonoBehaviour
{
    public UnityEvent onWalkBy;

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            if(onWalkBy != null)
                onWalkBy.Invoke();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(3, 0, 3, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
