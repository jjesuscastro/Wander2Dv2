using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        Canvas canvas;

        #region Singleton
        public static UIController instance;

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("Multiple ui controllers found");
            }
            instance = this;
        }
        #endregion

        void Start()
        {
            canvas = gameObject.GetComponent<Canvas>();
        }

        public void SetLevel()
        {
            Camera mainCamera = GameObject.Find("mainCamera").GetComponent<Camera>();
            if(canvas != null && mainCamera != null)
                canvas.worldCamera = mainCamera;
        }
    }
}