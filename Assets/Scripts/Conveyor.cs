using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    Vector3 tempVector;
    public float speed;

    void Update()
    {
        foreach(Transform child in transform)
        {
            tempVector = child.transform.position;
            tempVector.x += speed * Time.deltaTime;
            child.transform.position = tempVector;            
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            other.transform.parent = null;
        }
    }
}
