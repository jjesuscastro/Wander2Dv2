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
        public bool open = false;
        // JournalSlot[] slots;

        void Start()
        {
            journal = Journal.instance;
            gameObject.SetActive(false);
        }

        public void ToggleUI()
        {
            if(mc == null || npc == null)
            {
                PlayerMovement[] players = GameObject.FindObjectsOfType<PlayerMovement>();
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].gameObject.CompareTag("Player"))
                        mc = players[i];

                    if (players[i].gameObject.CompareTag("NPC"))
                        npc = players[i];
                }
            }

            if(mc.isActiveAndEnabled && !open)
            {
                mcEnabled = true;
                mc.enabled = false;
                open = true;
            } else if (npc.isActiveAndEnabled && !open){
                npcEnabled = true;
                npc.enabled = false;
                open = true;
            }

            if(mcEnabled && open)
            {
                mcEnabled = false;
                mc.enabled = true;
                open = false;
            } else if (npcEnabled && open) {
                npcEnabled = false;
                npc.enabled = true;
                open = false;
            }


            // currentPage = 0;
            // center.gameObject.SetActive(true);
            // left.gameObject.SetActive(false);
            // right.gameObject.SetActive(false);
            SetImages();
        }

        public void CenterPage()
        {
            Debug.Log("Center page clicked");
            if(currentPage <= 0)
                NextPage();
            else
                PreviousPage();
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

            Debug.Log("Next page");
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

            Debug.Log("Previous page");
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
