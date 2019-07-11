using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ShizoMentalHealth : MentalHealthEffect
    {
        public float fadeRate;
        public SpriteRenderer face;
        public AudioSource whispers;
        public bool fadeIn = true;
        public bool fadeOut = false;

        public override void Trigger()
        {
            base.Trigger();
            fadeIn = true;
            fadeOut = false;
            Debug.Log("Schizo MH");
        }

        public override void SetValues()
        {
            whispers = GameObject.Find("audioSource").GetComponent<AudioSource>();
        }

        public override void Stop()
        {
            base.Stop();
            fadeIn = false;
            fadeOut = true;
        }

        void Update()
        {
            Color tempColor;
            if (isEnabled)
            {
                if (fadeIn)
                {
                    whispers.volume += fadeRate;
                    tempColor = face.color;
                    tempColor.a += fadeRate;
                    face.color = tempColor;

                    if (tempColor.a >= 0.15)
                    {
                        fadeIn = false;
                    }

                }
            }
            else
            {
                if (fadeOut)
                {
                    whispers.volume -= fadeRate * 2;
                    tempColor = face.color;
                    tempColor.a -= fadeRate;
                    face.color = tempColor;
                    Debug.Log("Schizo disabled and fading out");

                    if (tempColor.a <= 0)
                    {
                        fadeOut = false;
                    }

                    fadeIn = true;
                }
            }
        }
    }
}