using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerMentalHealth : MonoBehaviour {

        public float health = 1f;

        [SerializeField] private MentalHealth mentalHealth;
        
        Vector3 checkPoint;
        bool criticalLevel = false;
        float timer = 0;

        void Update() {
            mentalHealth.SetSize(health);

            if(criticalLevel) {
                timer += Time.deltaTime;
                if(timer % 60 >= 5) {
                    resetLevel();
                }
            }
        }

        void resetLevel() {
            changeHealth(1f);
            GetComponentInParent<PlayerController>().respawn();
            transform.position = checkPoint;
        }

        void changeHealth(float damageValue) {
            health += damageValue;

            if(health >= 1) {
                health = 1;
                timer = 0;
            }
            
            if(health < 0f)
                health = 0f;

            if(health < 0.15f) {
                timer = 0;
                criticalLevel = true;
            }
            else {
                criticalLevel = false;
            }

            if(health <= 0) {
                resetLevel();
            }
        }
    }
}