using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object;

namespace GameManager
{
    public class Journal : MonoBehaviour
    {
        [SerializeField]
        private GameObject journalSlot;
        public Transform journalsParent;
        // public UnityEvent onEntryChanged;
        public List<Entry> entries = new List<Entry>();
        public Entry placeholder;

        #region Singleton
        public static Journal instance;

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("Multiple journals found");
            }
            instance = this;
        }
        #endregion

        public bool Add(Entry entry)
        {
            entries.Insert(entries.Count - 2, entry);
            if(entries.Count % 2 != 0)
                entries.Insert(entries.Count - 2, placeholder);
            else
                entries.Remove(placeholder);

            // if(onEntryChanged != null)
            //     onEntryChanged.Invoke();

            return true;
        }

        public void Remove(Entry entry)
        {
            entries.Remove(entry);
            
            // if(onEntryChanged != null)
            //     onEntryChanged.Invoke();
        }
    }
}
