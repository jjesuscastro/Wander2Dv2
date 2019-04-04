using UnityEngine;

namespace Object
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public string description = "Item description";
        public Sprite icon = null;
        public string useLocationName;
        [System.NonSerialized] public bool isUsed = false;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
            isUsed = true;
        }
    }
}
