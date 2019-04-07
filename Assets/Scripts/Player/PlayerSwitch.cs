using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Player
{
    public class PlayerSwitch : MonoBehaviour
    {
        private PlayerMovement mc;

        PlayerMovement npc;

        public bool obtainedNPC = false;
        public float maxDistance;

        CameraFollow mainCamera;
        PlayerMentalHealth playerMentalHealth;

        private Transform currentPlayer;

        #region Singleton
        public static PlayerSwitch instance;

        void Awake()
        {
            if(instance != null)
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
            currentPlayer = mc.transform;
            mainCamera = Camera.main.GetComponent<CameraFollow>();
            playerMentalHealth = GetComponent<PlayerMentalHealth>();
            npc.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Switch"))
            {
                if(obtainedNPC)
                {
                    if(mc.isActiveAndEnabled)
                    {
                        mc.enabled = false;
                        npc.enabled = true;
                        currentPlayer = npc.transform;
                        mc.GetComponent<PlayerMovement>().StopAnimation();
                        mc.GetComponent<CharacterController2D>().QueueDisableColliders();
                        npc.GetComponent<CharacterController2D>().EnableColliders();
                        playerMentalHealth.SwitchTarget(npc.gameObject);
                        mainCamera.switchTarget(npc);
                    }
                    else
                    {
                        mc.enabled = true;
                        npc.enabled = false;
                        currentPlayer = mc.transform;
                        npc.GetComponent<PlayerMovement>().StopAnimation();
                        npc.GetComponent<CharacterController2D>().QueueDisableColliders();
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
        }

        void CheckDistance()
        {
            if(mc != null && npc != null)
            {
                Transform mcTransform = mc.GetComponent<Transform>();
                Transform npcTransform = npc.GetComponent<Transform>();

                if(obtainedNPC)
                {
                    if(Vector3.Distance(mcTransform.position, npcTransform.position) > maxDistance)
                    {
                        if(mc.isActiveAndEnabled)
                        {
                            if(mc.GetComponent<CharacterController2D>().IsGrounded())
                            {
                                npcTransform.position = mcTransform.position;
                                npc.GetComponent<PlayerController>().FadeIn();
                            }
                        }
                        else
                        {
                            if(npc.GetComponent<CharacterController2D>().IsGrounded())
                            {
                                mcTransform.position = npcTransform.position;
                                mc.GetComponent<PlayerController>().FadeIn();
                            }
                        }
                    }
                }
            }
        }

        public void SetLevel()
        {
            PlayerMovement[] players = GameObject.FindObjectsOfType<PlayerMovement>();
            for(int i = 0; i < players.Length; i++)
            {
                if(players[i].name.CompareTo("mc") == 0)
                    mc = players[i];

                if(players[i].name.CompareTo("NPC") == 0)
                    npc = players[i];
            }
            obtainedNPC = false;
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