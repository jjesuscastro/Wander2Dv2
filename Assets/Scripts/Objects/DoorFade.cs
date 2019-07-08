using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DoorFade : MonoBehaviour
    {
        bool isFocused = false;
        bool hasInteracted = false;
        public float fadeRate;
        public int length;
        Transform player;
        public Transform newPosition;
        [SerializeField]
        private GameObject mainCharacter;
        [SerializeField]
        private GameObject nonPlayableCharacter;

        [SerializeField]
        GameObject cover;
        SpriteRenderer coverRenderer;
        bool fadecover;
        bool fadeIn;
        float timer = 0f;

        // Use this for initialization
        void Start()
        {
            mainCharacter = GameObject.Find("mc");
            nonPlayableCharacter = GameObject.Find("NPC");
            cover = GameObject.Find("coverBlack");
            coverRenderer = cover.gameObject.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isFocused && player != null && hasInteracted != true)
            {
                if (isFocused)
                {
                    hasInteracted = Interact();
                }
            }

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
                MovePlayers();
                timer = 0;
            }

            if (fadecover)
            {
                tmpColor = coverRenderer.color;
                tmpColor.a -= fadeRate;
                coverRenderer.color = tmpColor;
                if (tmpColor.a <= 0)
                    fadecover = false;
                fadeIn = false;
            }
        }

        void MovePlayers()
        {
            if (mainCharacter != null)
                mainCharacter.transform.position = newPosition.position;

            if (nonPlayableCharacter != null)
                nonPlayableCharacter.transform.position = newPosition.position;
        }

        public void OnFocus(Transform playerTransform)
        {
            isFocused = true;
            player = playerTransform;
        }

        public void OnDefocus()
        {
            isFocused = false;
            player = null;
            hasInteracted = false;
        }

        public bool Interact()
        {
            if (Input.GetButtonDown("Interact"))
            {
                fadeIn = true;

                return true;
            }

            return false;
        }
    }

}