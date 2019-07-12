using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnxietyMentalHealth : MentalHealthEffect
    {

        public float fadeRate;
        public float minEnabledTime;
        public float maxEnabledTime;
        public float minDisabledTime;
        public float maxDisabledTime;
        public SpriteRenderer whiteBackground;
        public SpriteRenderer intenseVignette;
        public GameObject blackCover;
        bool fadeIn = true;
        bool fadeOut;
        bool firstCall = true;

        public override void SetValues()
        {
            blackCover = null;
            whiteBackground = GameObject.Find("whiteBG").GetComponent<SpriteRenderer>();
            intenseVignette = GameObject.Find("vignette2").GetComponent<SpriteRenderer>();
            blackCover = GameObject.Find("/mainCamera/coverBlack_1");
            blackCover.transform.SetParent(Camera.main.transform);
            blackCover.SetActive(false);
        }

        public override void Trigger()
        {
            base.Trigger();
            fadeIn = true;
            fadeOut = false;
            Debug.Log("Anxiety MH");
        }

        public override void Stop()
        {
            base.Stop();
            fadeIn = false;
            fadeOut = true;
            firstCall = true;
        }

        void Update()
        {
            Color tempColor;
            if (isEnabled)
            {
                if (fadeIn)
                {
                    tempColor = whiteBackground.color;
                    tempColor.a += fadeRate;
                    whiteBackground.color = tempColor;
                    intenseVignette.color = tempColor;

                    if (tempColor.a >= 1)
                    {
                        fadeIn = false;
                        fadeOut = true;
                    }
                }
            }
            else
            {
                if (fadeOut)
                {
                    tempColor = whiteBackground.color;
                    tempColor.a -= fadeRate;
                    whiteBackground.color = tempColor;
                    intenseVignette.color = tempColor;

                    if (tempColor.a <= 0)
                    {
                        fadeOut = false;
                        fadeIn = true;
                    }
                }
            }

            if (firstCall)
            {
                float delay = 10;
                if (isEnabled)
                    delay = Random.Range(minEnabledTime, maxEnabledTime);
                else
                    delay = Random.Range(minDisabledTime, maxDisabledTime);
                Invoke("Toggle", delay);
                firstCall = false;
            }
        }

        void Toggle()
        {
            if(blackCover == null)
            {
                SetValues();
            } else {
                if (blackCover.active)
                    blackCover.SetActive(false);
                else
                    blackCover.SetActive(true);

                Debug.Log(blackCover.name);

                float delay = 10;
                if (isEnabled)
                    delay = Random.Range(minEnabledTime, maxEnabledTime);
                else
                {
                    delay = Random.Range(minDisabledTime, maxDisabledTime);

                    if (blackCover.active)
                        delay = 0.5f;
                }
                Invoke("Toggle", delay);
            }
        }
    }
}