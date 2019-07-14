using UnityEngine;
using GameManager;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public Transform itemsParent;
        Inventory inventory;
        InventorySlot[] slots;

        #region Singleton
        public static InventoryUI instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple Inventory UIs found");
            }
            instance = this;
        }
        #endregion

        void Start()
        {
            inventory = Inventory.instance;
            slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            // gameObject.SetActive(false);
        }

        public void UpdateUI()
        {
            if (slots == null)
            {
                slots = itemsParent.GetComponentsInChildren<InventorySlot>();
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddItem(inventory.items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
}