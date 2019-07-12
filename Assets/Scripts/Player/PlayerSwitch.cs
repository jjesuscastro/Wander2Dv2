﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Player
{
    public class PlayerSwitch : MonoBehaviour
    {
        public PlayerMovement mc;

        public PlayerMovement npc;

        public bool obtainedNPC = false;
        public float minDistance;
        public float distanceToStop = 1;
        bool npcIsFollowing = false;
        bool mcIsFollowing = false;
        bool locatingPositionForMC = false;
        bool locatingPositionForNPC = false;
        public bool followActive = true;

        public CameraFollow mainCamera;
        public PlayerMentalHealth playerMentalHealth;

        public Transform currentPlayer;
        public Transform mcTransform = null;
        public Transform npcTransform = null;

        #region Singleton
        public static PlayerSwitch instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple player switches found");
            }
            instance = this;
        }
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            SetLevel();
        }

        public void ReEnableSwitch()
        {
            obtainedNPC = true;
        }

        public void ForceSwitchToMC()
        {
            if (mc.isActiveAndEnabled)
            {
                obtainedNPC = false;
            }
            else
            {
                obtainedNPC = false;
                mc.enabled = true;
                mcIsFollowing = false;
                npc.enabled = false;
                currentPlayer = mc.transform;
                npc.GetComponent<PlayerMovement>().StopAnimation();
                // npc.GetComponent<CharacterController2D>().QueueDisableColliders();
                mc.GetComponent<CharacterController2D>().EnableColliders();
                playerMentalHealth.SwitchTarget(mc.gameObject);
                mainCamera.switchTarget(mc);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (locatingPositionForNPC)
            {
                Vector3 newPosition = npcTransform.position;
                if(npc.GetComponent<CharacterController2D>().IsGrounded())
                {
                    npc.GetComponent<CharacterController2D>().QueueDisableColliders();
                    newPosition.x += 1;
                    if(newPosition.x == mcTransform.position.x)
                    {
                        newPosition.x -= 1;
                        newPosition.y += 1;
                    }
                    npcTransform.position = newPosition;
                } else {
                    npc.GetComponent<CharacterController2D>().EnableColliders();
                    locatingPositionForNPC = false;
                }
            }

            if (locatingPositionForMC)
            {
                Vector3 newPosition = mcTransform.position;
                if(mc.GetComponent<CharacterController2D>().IsGrounded())
                {
                    mc.GetComponent<CharacterController2D>().QueueDisableColliders();
                    newPosition.x += 1;
                    if(newPosition.x == npcTransform.position.x)
                    {
                        newPosition.x -= 1;
                        newPosition.y += 1;
                    }
                    mcTransform.position = newPosition;
                } else {
                    mc.GetComponent<CharacterController2D>().EnableColliders();
                    locatingPositionForMC = false;
                }
            }

            if (Input.GetButtonDown("Switch"))
            {
                if (obtainedNPC)
                {
                    if (mc.isActiveAndEnabled)
                    {
                        mc.enabled = false;
                        npcIsFollowing = false;
                        npc.enabled = true;
                        currentPlayer = npc.transform;
                        mc.GetComponent<PlayerMovement>().StopAnimation();
                        // mc.GetComponent<CharacterController2D>().QueueDisableColliders();
                        npc.GetComponent<CharacterController2D>().EnableColliders();
                        playerMentalHealth.SwitchTarget(npc.gameObject);
                        mainCamera.switchTarget(npc);
                    }
                    else
                    {
                        mc.enabled = true;
                        mcIsFollowing = false;
                        npc.enabled = false;
                        currentPlayer = mc.transform;
                        npc.GetComponent<PlayerMovement>().StopAnimation();
                        // npc.GetComponent<CharacterController2D>().QueueDisableColliders();
                        mc.GetComponent<CharacterController2D>().EnableColliders();
                        playerMentalHealth.SwitchTarget(mc.gameObject);
                        mainCamera.switchTarget(mc);
                    }
                }
                else
                {
                    Debug.Log("You have not obtained the NPC yet!");
                }
            }

            CheckDistance();
            Follow();
        }

        void Follow()
        {            
            if(mc == null || npc == null)
            {
                mcTransform = mc.GetComponent<Transform>();
                npcTransform = npc.GetComponent<Transform>();
            }


            if (npcIsFollowing)
            {
                Debug.Log("NPC is following");
                if (Vector3.Distance(mcTransform.position, npcTransform.position) > distanceToStop)
                {
                    if (npc.GetComponent<CharacterController2D>().IsGrounded())
                    {
                        npc.getAnimator().SetBool("Walk", true);
                        npc.GetComponent<CharacterController2D>().Move(npc.GetComponent<PlayerMovement>().getMoveSpeed() * Time.fixedDeltaTime, 0, false, false);
                    }

                }
                else
                {
                    npc.getAnimator().SetBool("Walk", false);
                    npcIsFollowing = false;
                }
            }

            if (mcIsFollowing)
            {
                Debug.Log("MC is following");
                if (Vector3.Distance(npcTransform.position, mcTransform.position) > distanceToStop)
                {
                    if (mc.GetComponent<CharacterController2D>().IsGrounded())
                    {
                        mc.getAnimator().SetBool("Walk", true);
                        mc.GetComponent<CharacterController2D>().Move(mc.GetComponent<PlayerMovement>().getMoveSpeed() * Time.fixedDeltaTime, 0, false, false);
                    }

                }
                else
                {
                    mc.getAnimator().SetBool("Walk", false);
                    mcIsFollowing = false;
                }
            }
        }

        public void DisableFollow()
        {
            followActive = false;
        }

        public void EnableFollow()
        {
            followActive = true;
        }

        void CheckDistance()
        {
            if (mc != null && npc != null)
            {
                if(mcTransform == null || npcTransform == null)
                {
                    mcTransform = mc.GetComponent<Transform>();
                    npcTransform = npc.GetComponent<Transform>();
                }

                if (obtainedNPC)
                {
                    if (Vector3.Distance(mcTransform.position, npcTransform.position) > minDistance)
                    {
                        /*
                        What we want to happen:
                            minDistance: adjust this so it's far enough to be "off-screen"
                            1. When active character walks, inactive character just stands in place.
                            2. When active character stops, inactive character "walks" in screen.
                            3. IF inactive character will NOT walk if active character stops while
                               inactive character is still in-frame.
                         */
                        if (followActive && (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
                        {
                            //test
                            if (mc.isActiveAndEnabled)
                            {
                                if (mc.GetComponent<CharacterController2D>().IsGrounded() && npc.transform.position.x < mc.transform.position.x)
                                {
                                    //This is where NPC "follows" MC
                                    Vector3 newPosition = mcTransform.position;
                                    newPosition.x -= 25;
                                    newPosition.y += 20;
                                    npc.GetComponent<PlayerController>().RemoveColor();
                                    npc.GetComponent<PlayerController>().WalkIn();
                                    npcTransform.position = newPosition;
                                    locatingPositionForNPC = true;

                                    npcIsFollowing = true;
                                }
                            }
                            else
                            {
                                if (npc.GetComponent<CharacterController2D>().IsGrounded() && mc.transform.position.x < npc.transform.position.x)
                                {
                                    //This is where MC "follows" NPC
                                    Vector3 newPosition = npcTransform.position;
                                    newPosition.x -= 8;
                                    newPosition.y += 5;
                                    mc.GetComponent<PlayerController>().RemoveColor();
                                    mc.GetComponent<PlayerController>().WalkIn();
                                    mcTransform.position = newPosition;
                                    locatingPositionForMC = true;

                                    mcIsFollowing = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void SetLevel()
        {
            PlayerMovement[] players = GameObject.FindObjectsOfType<PlayerMovement>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].gameObject.CompareTag("Player"))
                    mc = players[i];

                if (players[i].gameObject.CompareTag("NPC"))
                    npc = players[i];
            }
            obtainedNPC = false;

            currentPlayer = mc.transform;
            mainCamera = Camera.main.GetComponent<CameraFollow>();
            playerMentalHealth = GetComponent<PlayerMentalHealth>();
            npc.gameObject.SetActive(false);
        }

        public void SetLevel(string sceneName)
        {
            PlayerMovement[] players = GameObject.FindObjectsOfType<PlayerMovement>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].gameObject.CompareTag("Player"))
                    mc = players[i];

                if (players[i].gameObject.CompareTag("NPC"))
                    npc = players[i];
            }
            obtainedNPC = false;

            currentPlayer = mc.transform;
            mainCamera = Camera.main.GetComponent<CameraFollow>();
            playerMentalHealth = GetComponent<PlayerMentalHealth>();
            npc.gameObject.SetActive(false);
            if(sceneName == "Mood")
                followActive = true;
            else
                followActive = false;
        }

        public void ObtainedNPC()
        {
            obtainedNPC = true;
        }

        public Transform GetCurrentPlayer()
        {
            return currentPlayer;
        }
    }
}
