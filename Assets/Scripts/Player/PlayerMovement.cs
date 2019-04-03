using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 30f;

        [SerializeField]
        CharacterController2D controller;

        [HideInInspector]
        public Vector2 playerInput;

        float move = 0;
        bool jump = false;
        bool crouch = false;
        Animator animator;

        // Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            move = Input.GetAxisRaw("Horizontal") * moveSpeed;
            playerInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

            if (move != 0)
            {
                animator.SetBool("Walk", true);
            } else
            {
                animator.SetBool("Walk", false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("Walk", false);
                animator.SetBool("isJumping", true);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            } else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }

        public void OnLanding()
        {
            animator.SetBool("isJumping", false);
        }

        void FixedUpdate()
        {
            controller.Move(move * Time.fixedDeltaTime, crouch, jump);

            jump = false;
        }

        public void StopAnimation()
        {
            animator.SetBool("Walk", false);
        }
    }
}