using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace UI
{
    public class JournalButton : MonoBehaviour
    {
        public Sprite newIcon;
        Sprite defaultIcon;
        Image btnImage;

        #region Singleton
        public static JournalButton instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple journal buttons found");
            }
            instance = this;
        }
        #endregion

        void Start(){
            btnImage = gameObject.GetComponent<Image>();
            defaultIcon = btnImage.sprite;
        }

        public void NewJournal(){
            btnImage.sprite = newIcon;
        }

        public void DefaultJournal(){
            btnImage.sprite = defaultIcon;
        }
    }
}

