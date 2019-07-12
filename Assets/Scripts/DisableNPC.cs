using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[RequireComponent(typeof(BoxCollider2D))]
public class DisableNPC : MonoBehaviour
{
    public PlayerMovement npc;
    public bool disableNPC = true;

    void DisNPC()
    {
        PlayerSwitch.instance.ForceSwitchToMC();
    }

    void EnableNPC()
    {
        PlayerSwitch.instance.ReEnableSwitch();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            if(disableNPC)
                DisNPC();
            else
                EnableNPC();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 0, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
