using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerSwitch : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement mc;

        [SerializeField]
        private PlayerMovement npc;

        public bool obtainedNPC = false;

        CameraFollow mainCamera;
        PlayerMentalHealth playerMentalHealth;
        // Start is called before the first frame update
        void Start()
        {
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
                        playerMentalHealth.SwitchTarget(npc.gameObject);
                        mainCamera.switchTarget(npc);
                    }
                    else
                    {
                        mc.enabled = true;
                        npc.enabled = false;
                        playerMentalHealth.SwitchTarget(mc.gameObject);
                        mainCamera.switchTarget(mc);
                    }
                }
                else
                {
                    Debug.Log("You have not obtained the NPC yet!");
                }
            }
        }

        public void ObtainedNPC()
        {
            obtainedNPC = true;
        }
    }
}