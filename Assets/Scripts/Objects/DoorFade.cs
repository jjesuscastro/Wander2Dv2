using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    public class DoorFade : Interactable
    {
        public float fadeRate;
        public int length;
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
        void Start () {
            coverRenderer = cover.gameObject.GetComponent<SpriteRenderer>();
        }
        
        // Update is called once per frame
        void Update () {
            base.Update();

            Color tmpColor;
            if(fadeIn)
            {
                tmpColor = coverRenderer.color;
                tmpColor.a += fadeRate;
                coverRenderer.color = tmpColor;
                timer += Time.deltaTime;
            }

            if(timer % 60 >= length)
            {
                fadecover = true;
                MovePlayers();
            }

            if(fadecover)
            {
                tmpColor = coverRenderer.color;
                tmpColor.a -= fadeRate;
                coverRenderer.color = tmpColor;
                fadeIn = false;
            }
        }

        void MovePlayers()
        {
            if(mainCharacter != null)
                mainCharacter.transform.position = newPosition.position;

            if(nonPlayableCharacter != null)
                nonPlayableCharacter.transform.position = newPosition.position;
        }

        public override bool Interact()
        {
            if(Input.GetButtonDown("Interact"))
            {
                fadeIn = true;
            
                return true;
            }

            return false;
        }
    }

}