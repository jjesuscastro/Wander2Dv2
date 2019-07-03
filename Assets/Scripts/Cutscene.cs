using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour
{
    public UnityEvent onBlackCover;
    public SpriteRenderer blackCover;
    public SpriteRenderer sceneRenderer;
    public Sprite[] cutscenes;

    int currScene = 0;
    float timer = 0;
    bool sceneStarting = false;
    bool startTimer = true;

    void Update()
    {
        if (sceneStarting)
        {
            Color color = blackCover.color;
            color.a += 0.025f;
            blackCover.color = color;
            timer += Time.deltaTime;
            startTimer = true;
        }

        if (startTimer)
        {
            if (timer % 60 > 1)
            {
                if (onBlackCover != null)
                {
                    onBlackCover.Invoke();
                }
                sceneStarting = false;
                timer = 0;
                startTimer = false;
            }
        }

        if (!sceneStarting)
        {
            Color color = blackCover.color;
            color.a -= 0.025f;
            if (color.a > 0)
                blackCover.color = color;
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
