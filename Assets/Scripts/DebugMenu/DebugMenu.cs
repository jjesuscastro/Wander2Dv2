using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    public Debugger debugger;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12) && !debugger.gameObject.activeInHierarchy)
        {
            debugger.gameObject.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.F12) && debugger.gameObject.activeInHierarchy)
        {
            debugger.gameObject.SetActive(false);
        }
    }
}
