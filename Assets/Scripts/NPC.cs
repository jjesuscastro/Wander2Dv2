using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    #region Singleton
        public static NPC instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple NPCs found");
            }
            instance = this;
        }
    #endregion
}
