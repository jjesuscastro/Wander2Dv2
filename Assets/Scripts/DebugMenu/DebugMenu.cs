using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    bool isOpen = false;
    public Debugger debugger;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12) && !isOpen)
        {
            debugger.gameObject.SetActive(true);
        } else if(Input.GetKeyDown(KeyCode.F12) && isOpen)
        {
            debugger.gameObject.SetActive(false);
        }
    }
}
