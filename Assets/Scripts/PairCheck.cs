using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class PairCheck : MonoBehaviour
{
    public UnityEvent onPair;
    bool mc = false;
    bool npc = false;
    bool called = false;

    void Update()
    {
        if(onPair != null && !called && mc && npc)
        {
            onPair.Invoke();
            called = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mc = true;
        }

        if (other.CompareTag("NPC"))
        {
            npc = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 3, 3, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
