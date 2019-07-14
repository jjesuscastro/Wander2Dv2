using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneRenderer : MonoBehaviour
{
    #region Singleton
        public static CutsceneRenderer instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple cutscene renderers found");
            }
            instance = this;
        }
    #endregion
}
