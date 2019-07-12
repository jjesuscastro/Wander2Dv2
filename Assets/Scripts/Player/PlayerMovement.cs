using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 30f;

        [SerializeField]
        CharacterController2D controller;

        [SerializeField]
        AudioSource audioSource;

        [HideInInspector]
        public Vector2 playerInput;

        float move = 0;
        float moveV = 0;
        bool isWalking = false;
        bool jump = false;
        bool crouch = false;
        Animator animator;

        public float jumpDelay = 75;
        bool jumpDisabled = false;
        float timer = 0;

        // Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public float getMoveSpeed()
        {
            return moveSpeed;
        }

        public Animator getAnimator()
        {
            return animator;
        }

        // Update is called once per frame
        void Update()
        {
            move = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveV = Input.GetAxisRaw("Vertical");
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (move != 0)
            {
                animator.SetBool("Walk", true);

                if (!isWalking)
                {
                    isWalking = true;
                    if (audioSource != null && !DialogueManager.instance.dialogueOpen)
                        audioSource.Play();
                }

            }
            else
            {
                animator.SetBool("Walk", false);
                isWalking = false;
                if (audioSource != null)
                    audioSource.Stop();
            }

            if (Input.GetButtonDown("Jump") && !jumpDisabled)
            {
                jump = true;
                jumpDisabled = true;
                isWalking = false;
                if (audioSource != null)
                    audioSource.Stop();
                animator.SetBool("Walk", false);
                animator.SetBool("isJumping", true);
            }

            if(DialogueManager.instance.dialogueOpen && audioSource != null)
            {
                audioSource.Stop();
            }

            if (jumpDisabled)
            {
                timer += Time.deltaTime * 100;
                if (timer >= jumpDelay)
                {
                    // animator.SetBool("isJumping", false);
                    jumpDisabled = false;
                    timer = 0;
                }
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
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
            controller.Move(move * Time.fixedDeltaTime, moveV, crouch, jump);

            jump = false;
        }

        public void StopAnimation()
        {
            animator.SetBool("Walk", false);
        }
    }
}