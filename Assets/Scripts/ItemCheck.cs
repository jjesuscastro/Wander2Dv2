using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameManager;
using Object;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemCheck : MonoBehaviour
{
    Inventory inventory;
    public List<Item> itemsToCheck;
    public UnityEvent onCheckComplete;
    int countCheck = 0;

    void Start()
    {
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void CheckItems()
    {
        foreach(Item item in itemsToCheck)
        {
            if(Inventory.instance.items.Contains(item))
            {
                Inventory.instance.items.Remove(item);
                if(Inventory.instance.onItemChanged != null)
                    Inventory.instance.onItemChanged.Invoke();

                countCheck++;
            }
        }

        if(itemsToCheck.Count == countCheck && onCheckComplete != null)
            onCheckComplete.Invoke();
        else
        {
            Debug.Log("Incomplete items");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            CheckItems();
        }
    }
}
