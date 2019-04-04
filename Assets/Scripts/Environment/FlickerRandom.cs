using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerRandom : MonoBehaviour
{

    public float minTime;
    public float maxTime;

    // Update is called once per frame
    void Update()
    {
        float delay = Random.Range(minTime, maxTime);
        Invoke("Toggle", delay);
    }

    void Toggle()
    {
        if(gameObject.active)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
