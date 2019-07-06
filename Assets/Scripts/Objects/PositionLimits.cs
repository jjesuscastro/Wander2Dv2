using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLimits : MonoBehaviour
{
    public bool limitX;
    public bool limitY;
    public bool instantiateValues;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        if(instantiateValues)
        {
            maxX = minX = gameObject.transform.position.x;
            maxY = minY = gameObject.transform.position.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        if(limitX)
        {
            if(gameObject.transform.position.x < minX)  
                position.x = minX;
            if(gameObject.transform.position.x > maxX)
                position.x = maxX;
        }

        if(limitY)
        {
            if(gameObject.transform.position.y < minY)
                position.y = minY;
            if(gameObject.transform.position.y > maxY)
                position.y = maxY;
        }

        gameObject.transform.position = position;
    }
}
