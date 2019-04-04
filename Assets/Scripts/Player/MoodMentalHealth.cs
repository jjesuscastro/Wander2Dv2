using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodMentalHealth : MentalHealthEffect
{
    public GameObject[] covers; //Slow
    
    public float maxAlpha = 0.75f;
    public float fadeRate;
    public int color;
    public int length = 5;
    bool firstCall = true;
    bool fadecover;
    bool callOnce = false;
    bool fadeIn = true;
    bool fadeInBlack = true;
    bool fadeInYellow;
    Color tempColor;
    float timer = 0f;
    bool isStop = false;

    public override void Trigger()
    {
        base.Trigger();
    }

    void Update()
    {
        if(isEnabled)
        {
            isStop = false;
            if(fadeIn)
            {
                //Fade in black
                if(fadeInBlack)
                {
                    if(callOnce)
                    {
                        Time.timeScale = 0.5f;
                        callOnce = false;
                    }
                    tempColor = covers[0].GetComponent<SpriteRenderer>().color;
                    tempColor.a +=fadeRate;
                    if(tempColor.a <= maxAlpha)
                        covers[0].GetComponent<SpriteRenderer>().color = tempColor;
                }

                //Fade in yellow
                if(fadeInYellow)
                {
                    if(callOnce)
                    {
                        Time.timeScale = 1.5f;
                        callOnce = false;
                    }
                    tempColor = covers[1].GetComponent<SpriteRenderer>().color;
                    tempColor.a +=fadeRate;
                    if(tempColor.a <= maxAlpha)
                        covers[1].GetComponent<SpriteRenderer>().color = tempColor;
                }

                timer += Time.deltaTime;
            }

            if(timer %60 >= length * Time.timeScale)
            {
                fadeIn = false;
                fadecover = true;
                timer = 0;
            }

            if(fadecover)
            {
                if(fadeInBlack) //Which means black is visible so supposedly yellow is not
                {
                    //if you are here it means fadeblack is true and fadeyellow should be false
                    tempColor = covers[0].GetComponent<SpriteRenderer>().color;
                    tempColor.a -=fadeRate;
                    covers[0].GetComponent<SpriteRenderer>().color = tempColor;
                    if(tempColor.a <= 0)
                    {
                        fadeInBlack = false;
                        fadeInYellow = true;
                        fadeIn = true;
                        fadecover = false;
                        callOnce = true;
                    }
                } else if(fadeInYellow)
                {
                    tempColor = covers[1].GetComponent<SpriteRenderer>().color;
                    tempColor.a -=fadeRate;
                    covers[1].GetComponent<SpriteRenderer>().color = tempColor;
                    if(tempColor.a <= 0)
                    {
                        fadeInBlack = true;
                        fadeInYellow = false;
                        fadeIn = true;
                        fadecover = false;
                        callOnce = true;
                    }
                }
            }
        } 
        
        if(!isEnabled && !isStop) 
        {
                Time.timeScale = 1f;
                tempColor = covers[0].GetComponent<SpriteRenderer>().color;
                tempColor.a -=fadeRate;
                covers[0].GetComponent<SpriteRenderer>().color = tempColor;

                tempColor = covers[1].GetComponent<SpriteRenderer>().color;
                tempColor.a -=fadeRate;
                covers[1].GetComponent<SpriteRenderer>().color = tempColor;

                fadeInBlack = true;
                fadeInYellow = false;
                fadeIn = true;
                fadecover = false;
                callOnce = true;
                
                if(covers[0].GetComponent<SpriteRenderer>().color.a <= 0 && covers[1].GetComponent<SpriteRenderer>().color.a <= 0)
                    isStop = true;
        }
    }
}
