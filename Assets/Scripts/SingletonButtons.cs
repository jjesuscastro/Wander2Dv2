using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using GameManager;

public class SingletonButtons : MonoBehaviour
{
    InventoryUI inventoryUI;
    JournalUI journalUI;
    IdleUI idleUI;
    DialogueManager dialogueManager;

    void Start()
    {
        inventoryUI = InventoryUI.instance;
        journalUI = JournalUI.instance;
        idleUI = IdleUI.instance;
        dialogueManager = DialogueManager.instance;
    }

    public void UpdateInventoryUI()
    {
        if(inventoryUI != null)
        {
            inventoryUI.UpdateUI();
        } else
        {
            inventoryUI = InventoryUI.instance;
        }
        
    }

    public void JournalUIToggle()
    {
        if(journalUI != null)
        {
            journalUI.ToggleUI();
        } else
        {
            journalUI = JournalUI.instance;
        }
    }

    public void IdleUISetActive(bool value)
    {
        if(idleUI != null)
        {
            idleUI.gameObject.SetActive(value);
        } else
        {
            idleUI = IdleUI.instance;
        }
    }

    public void DialogueManagerNextSentence()
    {
        if(dialogueManager != null)
        {
            dialogueManager.DisplayNextSentence();
        } else
        {
            dialogueManager = DialogueManager.instance;
        }
    }
}
