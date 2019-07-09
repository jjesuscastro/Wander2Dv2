using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FadeOutIn : MonoBehaviour
{
    public float fadeRate;
    public int length;

    [SerializeField]
    GameObject cover;
    SpriteRenderer coverRenderer;
    bool fadecover;
    bool fadeIn;
    float timer = 0f;

    // Use this for initialization
    void Start()
    {
        coverRenderer = cover.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color tmpColor;
        if (fadeIn)
        {
            tmpColor = coverRenderer.color;
            tmpColor.a += fadeRate;
            coverRenderer.color = tmpColor;
            timer += Time.deltaTime;
        }

        if (timer % 60 >= length)
        {
            fadecover = true;
        }

        if (fadecover)
        {
            tmpColor = coverRenderer.color;
            tmpColor.a -= fadeRate;
            coverRenderer.color = tmpColor;
            fadeIn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            fadeIn = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 3, 3, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
