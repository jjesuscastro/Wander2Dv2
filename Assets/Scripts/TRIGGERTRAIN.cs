using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRIGGERTRAIN : MonoBehaviour
{
    public HorizontalPlatform train;
    bool playerOnPlatform;

    void Update()
    {
        // Debug.Log(gameObject.transform.position.x);

        if(playerOnPlatform && Input.GetButtonDown("Interact"))
        {
            train.stop = false;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = false;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = false;
    }
}
