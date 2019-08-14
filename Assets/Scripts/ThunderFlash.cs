using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderFlash : MonoBehaviour
{
    public float fadeRate;
    public float minDisabledTime;
    public float maxDisabledTime;
    public GameObject whiteCover;
    bool firstCall = true;
    bool isEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if(isEnabled)
        {
            Color color = whiteCover.GetComponent<SpriteRenderer>().color;

            color.a -= fadeRate;
            if (color.a <= 0)
                color.a = 0;

            whiteCover.GetComponent<SpriteRenderer>().color = color;

            if (firstCall)
            {
                float delay = 10;
                delay = Random.Range(minDisabledTime, maxDisabledTime);
                Invoke("Toggle", delay);
                firstCall = false;
            }
        } else {
            Color color = whiteCover.GetComponent<SpriteRenderer>().color;

            color.a -= fadeRate;
            if (color.a <= 0)
                color.a = 0;

            whiteCover.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void Toggle()
    {
        Color color = whiteCover.GetComponent<SpriteRenderer>().color;
        color.a = 0.85f;
        whiteCover.GetComponent<SpriteRenderer>().color = color;

        float delay = 10;
        delay = Random.Range(minDisabledTime, maxDisabledTime);

        if(isEnabled)
            Invoke("Toggle", delay);
    }

    public void EnableStorm()
    {
        isEnabled = true;
        Debug.Log("[ThunderFlash.cs] - Enabled thunder storm.");
    }

    public void DisableStorm()
    {
        isEnabled = false;
        Debug.Log("[ThunderFlash.cs] - Disabled thunder storm.");
    }
}
