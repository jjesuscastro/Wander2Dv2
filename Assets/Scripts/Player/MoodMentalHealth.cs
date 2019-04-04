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
    bool fadeIn;
    Color tempColor;
    float timer = 0f;

    public override void Trigger()
    {
        base.Trigger();
    }

    void Update()
    {
        if(isEnabled)
        {
            int color = Random.Range(0, covers.Length-1); //0 is black; 1 is yellow

            if(firstCall)
            {
                Invoke("ChooseColor", 0);
                firstCall = false;
            }
        } else if(!isEnabled){
            firstCall = true;
        }
    }

    void StartEffect()
    {
        Debug.Log("ey");
        fadeIn = true;
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
                ChooseColor();
            }
        }
    }

    void ChooseColor()
    {
        int color = Random.Range(0, covers.Length-1); //0 is black; 1 is yellow

        StartEffect();
    }
}
