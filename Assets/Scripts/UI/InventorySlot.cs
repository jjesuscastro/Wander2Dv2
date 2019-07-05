using UnityEngine;
using UnityEngine.UI;
using Object;
using GameManager;
using Player;

namespace UI
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        public Button removeButton;
        public UseLocation useLocation;
        public bool hasUseLocation;
        Item item;

        public void AddItem(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
            removeButton.interactable = true;
            if (item.useLocationName != null || item.useLocationName != "")
            {
                useLocation = FindTransform(item.useLocationName);
                hasUseLocation = true;
            }
        }

        UseLocation FindTransform(string useLocationName)
        {
            UseLocation[] useLocations = GameObject.FindObjectsOfType<UseLocation>();

            for (int i = 0; i < useLocations.Length; i++)
            {
                if (useLocations[i].name.CompareTo(useLocationName) == 0)
                    return useLocations[i];
            }

            return null;
        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

        public void RemoveItem()
        {
            Debug.Log("Dropping item " + item.name);
            Inventory.instance.Remove(item);
        }

        public void UseItem()
        {
            if (item != null && !hasUseLocation)
            {
                item.Use();
            }
            else
            {
                if (useLocation != null && Vector3.Distance(PlayerSwitch.instance.GetCurrentPlayer().position, useLocation.transform.position) < useLocation.radius)
                {
                    item.Use();
                    Inventory.instance.Remove(item);
                }
            }
        }
    }
}
