using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object;

namespace GameManager
{
    public class Inventory : MonoBehaviour
    {
        public UnityEvent onItemChanged;
        public int size;
        public List<Item> items = new List<Item>();

        #region Singleton
        public static Inventory instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple inventories found");
            }
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        #endregion

        public bool Add(Item item)
        {
            if (items.Count >= size)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            if (onItemChanged != null)
                onItemChanged.Invoke();

            return true;
        }

        public void Remove(Item item)
        {
            items.Remove(item);

            if (onItemChanged != null)
                onItemChanged.Invoke();
        }
    }
}
