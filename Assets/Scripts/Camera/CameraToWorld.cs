using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToWorld : MonoBehaviour
{
    Camera camera;
    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void OnDrawGizmos()
    {
        camera = GetComponent<Camera>();
        
        Vector3 upperRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(upperRight, 1F);

        Vector3 upperLeft = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(upperLeft, 1F);

        Vector3 lowerRight = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(lowerRight, 1F);

        Vector3 lowerLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(lowerLeft, 1F);
    }

    public Vector3 UpperRight()
    {
        return camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }

    public Vector3 UpperLeft()
    {
        return camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));
    }

    public Vector3 LowerRight()
    {
        return camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
    }

    public Vector3 LowerLeft()
    {
        return camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
    }
}
