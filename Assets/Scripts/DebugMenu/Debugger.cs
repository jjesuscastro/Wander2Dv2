using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class Debugger : MonoBehaviour
{
    Canvas canvas;
    void Start()
    {
        canvas = gameObject.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }

    //Add debug log warnings

    // Update is called once per frame
    void Update()
    {
        if(canvas == null || canvas.worldCamera == null) {
            canvas = gameObject.GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
        }
    }

    public void StartCutscene()
    {
        Debug.LogWarning("[Debugger.cs] - Starting cutscene.");
        Cutscene.instance.StartScene();
    }

    public void LoadNextScene()
    {
        Debug.LogWarning("[Debugger.cs] - Loading next scene.");
        SceneController.instance.LoadNewScene();
    }

    public void LoadSpecificScene(string sceneName)
    {
        Debug.LogWarning("[Debugger.cs] - Loading scene: " + sceneName);
        SceneController.instance.LoadSpecificScene(sceneName);
    }
}
