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

    // Update is called once per frame
    void Update()
    {
        if(canvas == null) {
            canvas = gameObject.GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
        }
    }

    public void StartCutscene()
    {
        Cutscene.instance.StartScene();
    }
}
