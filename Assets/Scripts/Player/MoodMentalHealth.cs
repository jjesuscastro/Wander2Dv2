using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Player
{
    public class MoodMentalHealth : MentalHealthEffect
    {
        public GameObject[] covers; //Slow

        public float maxAlpha = 0.75f;
        public float fadeRate;
        public int length = 5;
        public PostProcessVolume ppVolume;
        // bool firstCall = true;
        bool fadecover;
        bool callOnce = false;
        bool fadeIn = true;
        bool fadeInBlack = true;
        bool fadeInYellow;
        float timer = 0f;
        bool isStop = false;
        ColorGrading colorGrading;

        public override void Trigger()
        {
            base.Trigger();            
        }

        public override void Stop()
        {
            base.Stop();
        }

        void Update()
        {
            if(ppVolume != null)
                ppVolume.profile.TryGetSettings(out colorGrading);
            if (isEnabled)
            {
                isStop = false;
                if (fadeIn)
                {
                    //Fade in black
                    if (fadeInBlack)
                    {
                        if (callOnce)
                        {
                            Time.timeScale = 0.5f;
                            callOnce = false;
                        }

                        colorGrading.saturation.value -= fadeRate;
                        if(colorGrading.saturation.value < -100)
                            colorGrading.saturation.value = -100;
                    }

                    //Fade in yellow
                    if (fadeInYellow)
                    {
                        if (callOnce)
                        {
                            Time.timeScale = 1.5f;
                            callOnce = false;
                        }

                        colorGrading.saturation.value += fadeRate;
                        if(colorGrading.saturation.value > 100)
                            colorGrading.saturation.value = 100;
                    }

                    timer += Time.deltaTime;
                }

                if (timer % 60 >= length * Time.timeScale)
                {
                    fadeIn = false;
                    fadecover = true;
                    timer = 0;
                }

                if (fadecover)
                {
                    if (fadeInBlack) //Which means black is visible so supposedly yellow is not
                    {
                        fadeInBlack = false;
                        fadeInYellow = true;
                        fadeIn = true;
                        fadecover = false;
                        callOnce = true;
                    }
                    else if (fadeInYellow)
                    {
                        fadeInBlack = true;
                        fadeInYellow = false;
                        fadeIn = true;
                        fadecover = false;
                        callOnce = true;
                    }
                }
            }

            if (!isEnabled && !isStop)
            {
                Time.timeScale = 1f;

                if(colorGrading.saturation.value < 0)
                {
                    colorGrading.saturation.value += fadeRate;
                        if(colorGrading.saturation.value > 0)
                            colorGrading.saturation.value = 0;
                }
                else if (colorGrading.saturation.value > 0)
                {
                    colorGrading.saturation.value -= fadeRate;
                        if(colorGrading.saturation.value < 0)
                            colorGrading.saturation.value = 0;
                }

                fadeInBlack = true;
                fadeInYellow = false;
                fadeIn = true;
                fadecover = false;
                callOnce = true;

                if (colorGrading.saturation.value == 0)
                    isStop = true;
            }
        }
    }
}