using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallEvent : MonoBehaviour
{
    public UnityEvent fallTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            if(fallTrigger != null)
                fallTrigger.Invoke();
        }
    }
}
