using UnityEngine;

namespace Object
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        new public string name = "New Item";
        public string description = "Item description";
        public Sprite icon = null;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
        }
    }
}
