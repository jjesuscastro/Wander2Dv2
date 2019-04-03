using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodMentalHealth : MentalHealthEffect
{
    public GameObject[] covers; //Slow
    
    public float minDuration = 0, maxDuration = 3;
    public int color;
    bool firstCall = true;

    public override void Trigger()
    {
        base.Trigger();
    }

    void Update()
    {
        if(isEnabled)
        {
            int color = Random.Range(0, covers.Length); //0 is black; 1 is yellow

            if(firstCall)
            {
                Invoke("StartEffect", 0);
                firstCall = false;
            }
        } else if(!isEnabled){
            firstCall = true;
        }
    }

    void StartEffect()
    {
        Color tempColor;
        if(isEnabled)
        {
            if(color == 0)
            {
                tempColor = covers[color].GetComponent<SpriteRenderer>().color;
            }
        }
    }
}
