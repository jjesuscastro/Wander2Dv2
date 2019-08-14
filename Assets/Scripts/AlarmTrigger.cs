using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    public bool turnOn = true;
    [SerializeField]
    private SpriteRenderer redOverlay;
    float fadeRate = 0.025f;
    bool delay = false;
    bool triggered = false;
    public UnityEvent onActive;
    public UnityEvent onDeactive;
    bool triggeredOnce = false;

    // Update is called once per frame
    void Update()
    {
        if(triggered && !turnOn)
        {
            turnOff();
        }

        if(!delay && triggered){
            if(turnOn){
                if(redOverlay != null)
                {
                    Color tempColor = redOverlay.color;
                    tempColor.a += fadeRate;
                    redOverlay.color = tempColor;
                    if(redOverlay.color.a >= 0.25f)
                        delay = true;

                    if(onActive != null && !triggeredOnce)
                    {
                        onActive.Invoke();
                        triggeredOnce = true;
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

    public void turnOff()
    {
        turnOn = false;
        if(redOverlay != null)
        {
            Color tempColor = redOverlay.color;
            tempColor.a -= fadeRate;
            redOverlay.color = tempColor;

            if(redOverlay.color.a <= 0){
                tempColor.a = 0;
                redOverlay.color = tempColor;
                triggered = false;
            }

            if(onDeactive != null)
                onDeactive.Invoke();
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
