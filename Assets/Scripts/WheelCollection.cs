using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WheelCollection : MonoBehaviour
{
    public UnityEvent collectedAll;
    public int collectedCount;

    // Update is called once per frame
    void Update()
    {
        if(collectedCount == 3)
        {
            if(collectedAll != null)
                collectedAll.Invoke();
            collectedCount += 1;

            Debug.Log("[WheelCollection.cs] - All wheels collected, invoking event.");
        }
    }

    public void CollectedWheel()
    {
        collectedCount += 1;
    }
}
