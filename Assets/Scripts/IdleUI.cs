using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleUI : MonoBehaviour
{
    #region Singleton
    public static IdleUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple Idle UIs found");
        }
        instance = this;
    }
    #endregion
}
