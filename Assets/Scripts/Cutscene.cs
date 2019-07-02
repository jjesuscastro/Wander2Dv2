using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public SpriteRenderer blackCover;
    public Sprite[] cutscenes;

    int currScene = 0;

    public void NextScene() {
        if(currScene != cutscenes.Length)
        {
            
        }
    }
}
