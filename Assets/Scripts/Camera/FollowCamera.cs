using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    // [SerializeField]
    private Transform cameraToFollow;
    // [SerializeField]
    // private BoxCollider2D collider2D;

    public bool followX;
    public bool followY;
    public float yOffset = 0;

    public float stopX;

    public float leftEdge;
    public float rightEdge;

    void Start()
    {
        cameraToFollow = Camera.main.transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = transform.position;

        if (followX && rightEdge < stopX)
        {
            newPos.x = cameraToFollow.position.x;
        }
        else if (rightEdge >= stopX)
        {
            followX = false;
        }

        if (followY)
        {
            newPos.y = cameraToFollow.position.y + yOffset;
        }

        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 10);

        // leftEdge = collider2D.bounds.center.x - collider2D.size.x/2;
        // rightEdge = collider2D.bounds.center.x + collider2D.size.x/2;

        // if(Input.GetKeyDown(KeyCode.Q))
        //     Debug.Log(transform.name + " right edge = " + rightEdge);
    }
}
