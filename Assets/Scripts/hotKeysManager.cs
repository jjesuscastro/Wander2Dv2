using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using GameManager;

public class hotKeysManager : MonoBehaviour
{
    bool isOpen = false;
    JournalUI journal;
    IdleUI idleUI;
    DialogueManager dManager;

    void Start()
    {
        journal = JournalUI.instance;
        idleUI = IdleUI.instance;
        dManager = DialogueManager.instance;
    }

    void Update()
    {
        if(journal == null || idleUI == null || dManager == null)
        {
            journal = JournalUI.instance;
            idleUI = IdleUI.instance;
            dManager = DialogueManager.instance;
        }
        if(Input.GetButtonDown("Journal") && !isOpen && !dManager.dialogueOpen)
        {
            journal.gameObject.SetActive(true);
            idleUI.gameObject.SetActive(false);
            journal.ToggleUI();
            isOpen = true;
        } else if(Input.GetButtonDown("Journal") && isOpen)
        {
            journal.gameObject.SetActive(false);
            idleUI.gameObject.SetActive(true);
            journal.CloseUI();
            isOpen = false;
        }
    }
}
