using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class hotKeysManager : MonoBehaviour
{
    bool isOpen = false;

    void Update()
    {
        if(Input.GetButtonDown("Journal") && !isOpen)
        {
            JournalUI.instance.gameObject.SetActive(true);
            IdleUI.instance.gameObject.SetActive(false);
            JournalUI.instance.ToggleUI();
            isOpen = true;
        } else if(Input.GetButtonDown("Journal") && isOpen)
        {
            JournalUI.instance.gameObject.SetActive(false);
            IdleUI.instance.gameObject.SetActive(true);
            JournalUI.instance.CloseUI();
            isOpen = false;
        }
    }
}
