using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cutscene : MonoBehaviour
{
    public UnityEvent onBlackCover;
    public SpriteRenderer blackCover;
    public SpriteRenderer sceneRenderer;
    public SpriteRenderer blankSceneRenderer;
    public Sprite[] cutscenes;

    public int currScene = 0;
    float timer = 0;
    float sceneTimer = 0;
    bool sceneStarting = false;
    bool startTimer = false;
    public bool scenePlaying = false;
    bool sceneChanging = false;

    void Update()
    {
        if (sceneStarting && !scenePlaying)
        {
            Color color = blackCover.color;
            color.a += 0.025f;
            blackCover.color = color;
            timer += Time.deltaTime;
            startTimer = true;
        }

        if (startTimer && !scenePlaying)
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
            if (color.a >= 0)
                blackCover.color = color;
        }

        if (scenePlaying && !sceneChanging)
        {
            Color color = blankSceneRenderer.color;
            color.a -= 0.025f;
            if (color.a >= 0)
                blankSceneRenderer.color = color;

            sceneTimer += Time.deltaTime;
            if (sceneTimer % 60 > 5)
            {
                sceneTimer = 0;
                sceneChanging = true;
            }
        }

        if (scenePlaying && sceneChanging)
        {
            Color color = blankSceneRenderer.color;
            color.a += 0.025f;
            blankSceneRenderer.color = color;

            if (color.a <= 1)
                blankSceneRenderer.color = color;

            sceneTimer += Time.deltaTime;
            if (sceneTimer % 60 > 1)
            {
                sceneTimer = 0;
                NextScene();
                sceneChanging = false;
            }
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
            scenePlaying = true;
        }
        else
        {
            scenePlaying = false;
        }
    }
}
