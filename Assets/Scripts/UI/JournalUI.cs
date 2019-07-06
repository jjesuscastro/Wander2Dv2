using UnityEngine;
using GameManager;

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
        // JournalSlot[] slots;

        void Start()
        {
            journal = Journal.instance;
            gameObject.SetActive(false);
        }

        public void ToggleUI()
        {
            currentPage = 0; uncomment to always start at cover page
            center.gameObject.SetActive(true);
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
        }

        public void CenterPage()
        {
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
