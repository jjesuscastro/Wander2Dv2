﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager
{
    public class DialogueManager : MonoBehaviour
    {
        private Queue<string> names;
        private Queue<string> sentences;

        [SerializeField]
        private Animator animator;

        public Text nameText;
        public Text sentenceText;

        #region Singleton
        public static DialogueManager instance;

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
    
        public void StartDialogue(Dialogue dialogue)
        {
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

            animator.SetBool("isOpen", true);
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
            animator.SetBool("isOpen", false);
        }
    }
}