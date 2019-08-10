using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public bool turnOn = true;
    [SerializeField]
    private SpriteRenderer redOverlay;
    float fadeRate = 0.025f;
    bool delay = false;
    bool triggered = false;

    // Update is called once per frame
    void Update()
    {
        if(!delay && triggered){
            if(turnOn){
                if(redOverlay != null)
                {
                    Color tempColor = redOverlay.color;
                    tempColor.a += fadeRate;
                    redOverlay.color = tempColor;
                    if(redOverlay.color.a >= 0.25f)
                        delay = true;
                }
            } else {
                if(redOverlay != null)
                {
                    Color tempColor = redOverlay.color;
                    tempColor.a -= fadeRate;
                    redOverlay.color = tempColor;

                    if(redOverlay.color.a <= 0){
                        tempColor.a = 0;
                        redOverlay.color = tempColor;
                    }
                }
            }
        } else if (delay)
        {
            if(redOverlay != null)
            {
                Color tempColor = redOverlay.color;
                tempColor.a -= fadeRate;
                redOverlay.color = tempColor;

                if(redOverlay.color.a <= 0){
                    tempColor.a = 0;
                    redOverlay.color = tempColor;
                    delay = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            triggered = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
