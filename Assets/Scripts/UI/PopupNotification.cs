using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class PopupNotification : MonoBehaviour
    {
        public float fadeRate;
        public Image popup;
        public TextMeshProUGUI popupText;
        public bool showUp = false;

        #region Singleton
        public static PopupNotification instance;

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("Multiple popup notifications found");
            }
            instance = this;
        }
        #endregion

        void Update()
        {
            Color tempColorPopup, tempColorText;
            if(showUp)
            {
                tempColorPopup = popup.color;
                tempColorPopup.a += fadeRate;

                if(tempColorPopup.a <= 0.33f)
                    popup.color = tempColorPopup;

                tempColorText = popupText.color;
                tempColorText.a += fadeRate * 3;

                if(tempColorText.a <= 1f)
                    popupText.color = tempColorText;
                else
                    Invoke("Hide", 1.5f);
            } else {

                if(popup.color.a > 0)
                {
                    tempColorPopup = popup.color;
                    tempColorPopup.a -= fadeRate;

                    popup.color = tempColorPopup;
                }

                if(popupText.color.a > 0)
                {
                    tempColorText = popupText.color;
                    tempColorText.a -= fadeRate * 3;
                    popupText.color = tempColorText;
                }
            }
        }

        public void ShowPopup(string newPopupText)
        {
            popupText.text = newPopupText;
            showUp = true;
        }

        void Hide()
        {
            showUp = false;
        }
    }
}