using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[RequireComponent(typeof(BoxCollider2D))]
public class MentalHealthEvent : MonoBehaviour
{
    float newHealth = 0.245f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            PlayerMentalHealth.instance.setHealth(newHealth);
            Debug.Log("[MentalHealthEvent.cs] - Health changed to: " + newHealth + " from: " + this.name);
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
