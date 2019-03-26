using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class Dialogue : MonoBehaviour
    {
        public string[] names;

        [TextArea(3, 10)]
        public string[] sentences;

        public void TriggerDialogue()
        {
            DialogueManager.instance.StartDialogue(this);
        }
    }
}
