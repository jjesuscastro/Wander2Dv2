using UnityEngine;
using UnityEngine.UI;
using Object;
using GameManager;

namespace UI
{
    public class InventorySlot : MonoBehaviour
    {
        public Image itemPreview;
        public Text itemDesc;
        public Image icon;
        public Button removeButton;
        Item item;

        public void AddItem(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
            removeButton.interactable = true;
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
            if(item != null)
            {
                item.Use();
            }
        }

        void OnMouseOver()
        {
            if(item != null)
            {
                itemPreview.sprite = item.icon;
                itemPreview.enabled = true;
                itemDesc.text = item.description;
                // Debug.Log("Viewing " + item.name);
            }
        }

        void OnMouseExit()
        {
            itemPreview.enabled = false;
            itemPreview.sprite = null;
            itemDesc.text = null;
        }
    }
}
