using UnityEngine;
using UnityEngine.UI;
using Object;
using GameManager;

namespace UI
{
    public class JournalSlot : MonoBehaviour
    {
        public Image icon;

        public void SetImage(Sprite newEntry)
        {
            icon.sprite = newEntry;
            icon.enabled = true;
        }

        public void RemoveImage()
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
