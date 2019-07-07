using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float speed;
    public bool moveRight = true;
    public bool oneDirection = false;
    public bool stop = false;

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if (transform.localPosition.x >= maxX)
            {
                moveRight = false;
                if (oneDirection)
                    stop = true;
            }
            
            if (transform.localPosition.x <= minX)
            {
                moveRight = true;
                if (oneDirection)
                    stop = true;
            }

            if (moveRight)
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            else
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    public void Reset()
    {
        if(transform.localPosition.x > minX)
        {
            moveRight = false;
            oneDirection = true;
        } else
        {
            moveRight = true;
            oneDirection = true;
        }
    }
}
