using UnityEngine;
using GameManager;
using Player;

namespace UI
{
    public class JournalUI : MonoBehaviour
    {
        public Transform journalsParent;
        public JournalSlot center;
        public JournalSlot left;
        public JournalSlot right;
        Journal journal;
        public int currentPage = 0;
        public PlayerMovement mc;
        public PlayerMovement npc;
        bool mcEnabled  = false;
        bool npcEnabled = false;
        // JournalSlot[] slots;

        #region Singleton
        public static JournalUI instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple Journal UIs found");
            }
            instance = this;
        }
        #endregion

        void Start()
        {
            journal = Journal.instance;
            gameObject.SetActive(false);
        }

        public void ToggleUI()
        {
            if(mc == null || npc == null)
            {
                mc = MC.instance.gameObject.GetComponent<PlayerMovement>();
                npc = NPC.instance.gameObject.GetComponent<PlayerMovement>();
            }

            if(mc.isActiveAndEnabled)
            {
                mcEnabled = true;
                mc.enabled = false;
            } else if (npc.isActiveAndEnabled){
                npcEnabled = true;
                npc.enabled = false;
            }
            SetImages();
        }

        public void CloseUI()
        {
            if(mcEnabled)
            {
                mcEnabled = false;
                mc.enabled = true;
            } else if (npcEnabled) {
                npcEnabled = false;
                npc.enabled = true;
            }
        }

        public void CenterPage()
        {
            if(currentPage <= 0)
            {
                NextPage();
                Debug.Log("[JournalUI.cs] - CenterPage clicked. Next Page.");
            }
            else
            {
                PreviousPage();
                Debug.Log("[JournalUI.cs] - CenterPage clicked. Previous Page.");
            }
        }

        public void NextPage()
        {
            currentPage += 2;

            if (currentPage > 0)
            {
                center.gameObject.SetActive(false);
                left.gameObject.SetActive(true);
                right.gameObject.SetActive(true);
            }

            if (currentPage > journal.entries.Count - 1)
            {
                currentPage = journal.entries.Count - 1;
                center.gameObject.SetActive(true);
                left.gameObject.SetActive(false);
                right.gameObject.SetActive(false);
            }

            SetImages();

            Debug.Log("[JournalUI.cs] - Next Page.");
        }

        public void PreviousPage()
        {
            if(currentPage %2 == 0)
                currentPage -= 2;
            else
                currentPage -= 1;

            if (currentPage <= 0)
            {
                currentPage = 0;
                center.gameObject.SetActive(true);
                left.gameObject.SetActive(false);
                right.gameObject.SetActive(false);
            }

            if (currentPage < journal.entries.Count - 1 && currentPage > 0)
            {
                center.gameObject.SetActive(false);
                left.gameObject.SetActive(true);
                right.gameObject.SetActive(true);
            }

            SetImages();

            Debug.Log("[JournalUI.cs] - Previous Page.");
        }

        void SetImages()
        {
            if (currentPage <= 0)
            {
                currentPage = 0;
                center.SetImage(journal.entries[currentPage].icon);
            }

            if (currentPage > journal.entries.Count - 1)
                currentPage = journal.entries.Count - 1;

            if (currentPage == journal.entries.Count - 1)
                center.SetImage(journal.entries[currentPage].icon);

            right.SetImage(journal.entries[currentPage].icon);

            if (currentPage > 0)
                left.SetImage(journal.entries[currentPage - 1].icon);
        }

        // public void UpdateUI()
        // {
        //     slots = journalsParent.GetComponentsInChildren<JournalSlot>();

        //     for(int i = 0; i < slots.Length; i++)
        //     {
        //         if(i < journal.entries.Count)
        //         {
        //             slots[i].AddEntry(journal.entries[i]);
        //         } else
        //         {
        //             slots[i].ClearSlot();
        //         }
        //     }
        // }
    }
}
