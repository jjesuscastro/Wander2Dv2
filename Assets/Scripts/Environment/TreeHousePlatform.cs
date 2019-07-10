using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHousePlatform : MonoBehaviour
{
    public bool hasObject = false;
    public bool hasPlayer = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Weight"))
        {
            hasObject = true;
            other.transform.SetParent(this.transform);
        }

        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            hasPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Weight"))
        {
            hasObject = false;
            other.transform.parent = null;
        }

        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("NPC"))
        {
            hasPlayer = false;
        }
    }
}
