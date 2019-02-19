using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField]
    private Transform cameraToFollow;
    [SerializeField]
    private BoxCollider2D collider2D;

    public bool followX;
    public bool followY;

    public float stopX;

    public float leftEdge;
    public float rightEdge;

    void Start()
    {
        cameraToFollow = Camera.main.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;

        if(followX && rightEdge < stopX)
        {
            newPos.x = cameraToFollow.position.x;
        } else if(rightEdge >= stopX)
        {
            followX = false;
        }

        if(followY)
        {
            newPos.y = cameraToFollow.position.y;
        }

        transform.position = newPos;

        leftEdge = collider2D.bounds.center.x - collider2D.size.x/2;
        rightEdge = collider2D.bounds.center.x + collider2D.size.x/2;

        if(Input.GetKeyDown(KeyCode.Q))
            Debug.Log(transform.name + " right edge = " + rightEdge);
    }
}
