using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameManager
{
    public class DialogueManager : MonoBehaviour
    {
        private Queue<string> names;
        private Queue<string> sentences;

        [SerializeField]
        private Animator animator;

        public TextMeshProUGUI nameText;
        public TextMeshProUGUI sentenceText;

        #region Singleton
        public static DialogueManager instance;
        bool dialogueOpen = false;

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("Multiple dialogue managers found");
            }
            instance = this;
        }
        #endregion

        void Start()
        {
            names = new Queue<string>();
            sentences = new Queue<string>();
        }

        void Update()
        {
            if(dialogueOpen && Input.GetButtonDown("NextDialogue"))
                DisplayNextSentence();
        }
    
        public void StartDialogue(Dialogue dialogue)
        {
            dialogueOpen = true;
            Time.timeScale = 0.0001f;
            animator.speed = 10000f;
            names.Clear();
            sentences.Clear();

            foreach(string name in dialogue.names)
            {
                names.Enqueue(name);
            }

            foreach(string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            animator.SetBool("IsOpen", true);
            // Debug.Log("start " + animator.GetInteger("IsOpen"));
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if(names.Count == 0 || sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string name = names.Dequeue();
            string sentence = sentences.Dequeue();

            nameText.text = name;
            sentenceText.text = sentence;
        }

        void EndDialogue()
        {
            // Debug.Log("end " + animator.GetInteger("IsOpen"));
            Time.timeScale = 1f;
            animator.speed = 1;
            animator.SetBool("IsOpen", false);
            dialogueOpen = false;
        }
    }
}