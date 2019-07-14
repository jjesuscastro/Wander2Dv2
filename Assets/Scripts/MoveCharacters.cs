using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

[RequireComponent(typeof(BoxCollider2D))]
public class MoveCharacters : MonoBehaviour
{
    PlayerSwitch playerSwitch;
    PlayerController mc;
    PlayerController npc;

    void Start()
    {
        Debug.Log("EYYO FUCK: " + MC.instance);
        playerSwitch = PlayerSwitch.instance;
        mc = MC.instance.gameObject.GetComponent<PlayerController>();
        npc = NPC.instance.gameObject.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            mc.RemoveColor();
            mc.transform.position = npc.transform.position;
            mc.WalkIn();
        } else if(other.CompareTag("Player") && playerSwitch.obtainedNPC) {
            npc.RemoveColor();
            npc.transform.position = mc.transform.position;
            npc.WalkIn();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
