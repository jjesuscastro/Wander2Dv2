using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalHealthEffect : MonoBehaviour
{
    public bool isEnabled = true;

    public virtual void Trigger()
    {
        isEnabled = true;
        Debug.Log("[MentalHealthEffect.cs] - Trigger MH Effect: " + name);
    }

    public virtual void Stop()
    {
        isEnabled = false;
    }

    public virtual void SetValues()
    {
        Debug.Log("[MentalHealthEffect.cs] - Set MH values.");
    }
}
