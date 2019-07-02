using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public SpriteRenderer blackCover;
    public SpriteRenderer sceneRenderer;
    public Sprite[] cutscenes;

    int currScene = 0;
    bool sceneStarting = false;

    void Update()
    {
        if (sceneStarting)
        {
            Color color = blackCover.color;
            color.a += 0.025f;
        }
    }

    public void StartScene()
    {
        Color color = blackCover.color;
        color.a = 0;
        blackCover.color = color;
        sceneStarting = true;
    }

    public void NextScene()
    {
        if (currScene != cutscenes.Length)
        {
            sceneRenderer.sprite = cutscenes[currScene];
            currScene++;
        }
    }
}
