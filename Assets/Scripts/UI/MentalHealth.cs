using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MentalHealth : MonoBehaviour
    {
        private Transform bar;
        private static SpriteRenderer brainColored;
        private static SpriteRenderer barSprite;
        // Use this for initialization
        void Start()
        {
            bar = transform.Find("bar");
            barSprite = bar.Find("barSprite").GetComponent<SpriteRenderer>();
            brainColored = transform.Find("brainIconLow").Find("brainIcon").GetComponent<SpriteRenderer>();
        }

        public void SetSize(float size)
        {
            Color tmpColor = brainColored.color;
            tmpColor.a = size;
            bar.localScale = new Vector3(size, 1f);
            brainColored.color = tmpColor;
            SetColor(size);
        }

        static void SetColor(float size)
        {
            if (size >= 1f)
                barSprite.color = new Color(0.1129405f, 0.8867924f, 0.1491174f, 1);
            else if (size >= 0.5f && size < 1f)
                barSprite.color = Color.yellow;
            else if (size >= 0.25f && size < 0.5f)
                barSprite.color = new Color(340.4449f, 0.552f, 0f, 1);
            else
                barSprite.color = Color.red;
        }
    }
}

