using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class EyeMovement : MonoBehaviour
{
    PlayerMovement mc;
    PlayerMovement npc;
    public GameObject activePlayer;
    public GameObject pupil;
    public BoxCollider2D pupilBounds;

    void Start(){
        mc = MC.instance.GetComponent<PlayerMovement>();
        npc = NPC.instance.GetComponent<PlayerMovement>();
    }

    void Update(){
        if(mc.isActiveAndEnabled)
            activePlayer = mc.gameObject;
        else if(npc.isActiveAndEnabled)
            activePlayer = npc.gameObject;
            
        Bounds boxBounds = pupilBounds.bounds;
        Vector2 topRight = new Vector2(boxBounds.center.x + boxBounds.extents.x, boxBounds.center.y + boxBounds.extents.y);
        Vector2 lowerLeft = new Vector2(boxBounds.center.x - boxBounds.extents.x, boxBounds.center.y - boxBounds.extents.y);

        Vector2 clampedPosition = pupil.transform.position;
        clampedPosition.x = Mathf.MoveTowards(Mathf.Clamp(clampedPosition.x, lowerLeft.x, topRight.x), activePlayer.transform.position.x, 0.8f * Time.deltaTime);
        clampedPosition.y = Mathf.MoveTowards(Mathf.Clamp(clampedPosition.y, lowerLeft.y, topRight.y), activePlayer.transform.position.y, 0.8f * Time.deltaTime);
        pupil.transform.position = clampedPosition;
    }
}
