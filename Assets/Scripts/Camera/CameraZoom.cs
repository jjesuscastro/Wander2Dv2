using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraZoom : MonoBehaviour
{

    public float newZoom;
    public float newVerticalOffset;
    public float newLookAheadDstX;

    public float duration = 1.0f;
    private float currentZoom;
    private float currentVerticaOffset;
    private float currentLookAheadDstX;
    private float elapsed = 0.0f;
    private bool transition = false;

    Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
    }

    void Update()
    {
        if (transition)
        {
            elapsed += (Time.deltaTime / duration);
            mainCamera.orthographicSize = Mathf.Lerp(currentZoom, newZoom, elapsed);
            mainCamera.GetComponent<CameraFollow>().verticalOffset = Mathf.Lerp(currentVerticaOffset, newVerticalOffset, elapsed);
            mainCamera.GetComponent<CameraFollow>().lookAheadDstX = Mathf.Lerp(currentLookAheadDstX, newLookAheadDstX, elapsed);
            if (elapsed > 1.0f)
            {
                transition = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // print("Player entered zoom");
            currentZoom = mainCamera.orthographicSize;
            currentVerticaOffset = mainCamera.GetComponent<CameraFollow>().verticalOffset;
            currentLookAheadDstX = mainCamera.GetComponent<CameraFollow>().lookAheadDstX;
            transition = true;
            elapsed = 0.0f;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 3, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
