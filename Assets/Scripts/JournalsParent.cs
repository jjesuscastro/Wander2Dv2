using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalsParent : MonoBehaviour
{
    #region Singleton
    public static JournalsParent instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Journals Parent found");
        }
        instance = this;
    }
    #endregion
}
