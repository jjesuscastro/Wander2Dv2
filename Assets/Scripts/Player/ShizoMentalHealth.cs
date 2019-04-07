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
        bool fadeIn = true;
        bool fadeOut;

        public override void Trigger()
        {
            base.Trigger();
            fadeIn = true;
            fadeOut = false;
            Debug.Log("Anxiety MH");
        }

        void Stop()
        {
            base.Stop();
            fadeIn = false;
            fadeOut = true;
        }

        void Update()
        {
            Color tempColor;
            if(isEnabled)
            {
                if(fadeIn)
                {
                    whispers.volume += fadeRate;
                    tempColor = face.color;
                    tempColor.a += fadeRate;
                    face.color = tempColor;

                    if(tempColor.a >= 0.15)
                    {
                        fadeIn = false;
                        fadeOut = true;
                    }
                }
            } else {
                if(fadeOut)
                {
                    whispers.volume -= fadeRate;
                    tempColor = face.color;
                    tempColor.a -= fadeRate;
                    face.color = tempColor;

                    if(tempColor.a <= 0)
                    {
                        fadeOut = false;
                        fadeIn = true;
                    }
                }
            }
        }
    }
}