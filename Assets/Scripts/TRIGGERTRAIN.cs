using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRIGGERTRAIN : MonoBehaviour
{
    public HorizontalPlatform train;
    float timer;
    bool startTimer;

    void Update()
    {
        // Debug.Log(gameObject.transform.position.x);

        if(timer % 60 > 1)
        {
            train.stop = false;
            timer = 0;
            startTimer = false;
        }

        if(startTimer)
        {
            timer += Time.deltaTime;
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
            startTimer = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            startTimer = true;
    }
}
