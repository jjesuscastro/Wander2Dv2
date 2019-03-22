using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFade : MonoBehaviour {

    public float fadeRate;
    public bool fadeIn = false;

	[SerializeField]
	GameObject objectToFade;
	SpriteRenderer[] objectToFadeRenderer;
    bool fadeobjectToFade = false;

	// Use this for initialization
	void Start () {
        objectToFadeRenderer = new SpriteRenderer[objectToFade.gameObject.GetComponents<SpriteRenderer>().Length + objectToFade.gameObject.GetComponentsInChildren<SpriteRenderer>().Length];
        objectToFade.gameObject.GetComponents<SpriteRenderer>().CopyTo(objectToFadeRenderer, 0);
        objectToFade.gameObject.GetComponentsInChildren<SpriteRenderer>().CopyTo(objectToFadeRenderer, objectToFade.gameObject.GetComponents<SpriteRenderer>().Length);
	}
	
	// Update is called once per frame
	void Update () {
        Color tmpColor;
		if(fadeobjectToFade)
        {
            for(int i = 0; i < objectToFadeRenderer.Length; i++)
            {
                tmpColor = objectToFadeRenderer[i].color;
                if(fadeIn)
                    if(tmpColor.a < 1)
                        tmpColor.a += fadeRate;
                    else
                        fadeobjectToFade = false;
                else
                    if(tmpColor.a > 0)
                        tmpColor.a -= fadeRate;
                    else
                        fadeobjectToFade = false;
                        
                objectToFadeRenderer[i].color = tmpColor;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            fadeobjectToFade = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 3, 3, .5f);
        Gizmos.DrawCube(GetComponent<BoxCollider2D>().bounds.center, new Vector2(GetComponent<BoxCollider2D>().transform.localScale.x, GetComponent<BoxCollider2D>().transform.localScale.y));
    }
}
