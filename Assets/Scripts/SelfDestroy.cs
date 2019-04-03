using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float destroyIn;

    // Update is called once per frame
    void Update()
    {
        Invoke("DestroySelf", destroyIn);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
