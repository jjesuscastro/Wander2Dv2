using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC : MonoBehaviour
{
    #region Singleton
        public static MC instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple MCs found");
            }
            instance = this;
        }
    #endregion
}
