using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class BasicFollow : MonoBehaviour
{
    PlayerMovement mc;
    public Transform mcTransform;
    public Transform npcTransform;

    void Start()
    {
        mc = mcTransform.gameObject.GetComponent<PlayerMovement>();
    }

    public void Follow()
    {
        Vector3 position;
        if (mc.isActiveAndEnabled)
        {
            position = mcTransform.position;
            position.y += 7;
            npcTransform.position = position;
        }
        else
        {
            position = npcTransform.position;
            position.y += 7;
            mcTransform.position = position;
        }
    }
}
