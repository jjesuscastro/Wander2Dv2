using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodMentalHealth : MentalHealthEffect
{
    public GameObject[] covers; //Slow
    
    public float fadeRate;
    public int color;
    int length = 5;
    bool firstCall = true;
    bool fadecover;
    bool fadeIn = true;
    bool fadeInBlack = true, fadeInYellow = false;
    Color tempColor;
    float timer = 0f;

    public override void Trigger()
    {
        base.Trigger();
    }

    void Update()
    {
        if(fadeInBlack)
            color = 0;
        
        if(fadeInYellow)
            color = 1;

        if(isEnabled)
        {

            if(fadeIn)
            {
                tempColor = covers[color].GetComponent<SpriteRenderer>().color;
                tempColor.a += fadeRate;
                covers[color].GetComponent<SpriteRenderer>().color = tempColor;
                timer += Time.deltaTime;
            }

            if(timer % 60 >= length)
            {
                fadecover = true;
            }

            if(fadecover)
            {
                tempColor = covers[color].GetComponent<SpriteRenderer>().color;
                tempColor.a -= fadeRate;
                covers[color].GetComponent<SpriteRenderer>().color = tempColor;

                fadeIn = false;
                
                if(fadeInBlack && tempColor.a <= 0)
                {
                    fadeInYellow = true;
                    fadeInBlack = false;
                }

                if(fadeInYellow && tempColor.a <= 0)
                {
                    fadeInBlack = true;
                    fadeInYellow = false;
                }
            }
        }
    }
}
