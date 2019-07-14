using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToWorld : MonoBehaviour
{
    Camera cam;
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void OnDrawGizmos()
    {
        cam = GetComponent<Camera>();

        Vector3 upperRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(upperRight, 1F);

        Vector3 upperLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(upperLeft, 1F);

        Vector3 lowerRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(lowerRight, 1F);

        Vector3 lowerLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(lowerLeft, 1F);
    }

    public Vector3 UpperRight()
    {
        return cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
    }

    public Vector3 UpperLeft()
    {
        return cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
    }

    public Vector3 LowerRight()
    {
        return cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));
    }

    public Vector3 LowerLeft()
    {
        return cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    }
}
