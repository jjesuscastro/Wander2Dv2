using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    Vector3 tempVector;
    public float speed;

    void Update()
    {
        foreach(Transform child in transform)
        {
            tempVector = child.transform.position;
            tempVector.x += speed * Time.deltaTime;
            child.transform.position = tempVector;            
        }
    }
}
