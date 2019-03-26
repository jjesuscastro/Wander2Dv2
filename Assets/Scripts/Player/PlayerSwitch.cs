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
        // Start is called before the first frame update
        void Start()
        {
            mainCamera = Camera.main.GetComponent<CameraFollow>();
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
                        mainCamera.switchTarget(npc);
                    }
                    else
                    {
                        mc.enabled = true;
                        npc.enabled = false;
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