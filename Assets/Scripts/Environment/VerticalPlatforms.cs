using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatforms : MonoBehaviour
{
    public float maxY;
    public float minY;
    public float speed;
    public bool moveUp = true;

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y >= maxY)
            moveUp = false;
        if (transform.localPosition.y <= minY)
            moveUp = true;

        if (moveUp)
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
    }
}
