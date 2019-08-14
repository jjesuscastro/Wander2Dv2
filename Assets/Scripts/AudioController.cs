using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audio;
    bool fadeOut = false;
    bool fadeIn = false;

    // Update is called once per frame
    void Update()
    {
        if(fadeOut)
        {
            audio[0].volume -= 0.01f;
            if(audio[0].volume <= 0)
            {
                audio[0].volume = 0;
                fadeOut = false;
            }
        }

        if(fadeIn)
        {
            audio[0].volume += 0.01f;
            if(audio[0].volume >= 1)
            {
                audio[0].volume = 1;
                fadeIn = false;
            }
        }
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    public void FadeIn()
    {
        fadeIn = true;
    }
}
