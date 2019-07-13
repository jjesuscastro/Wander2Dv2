using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerMentalHealth : MonoBehaviour
    {

        public float health = 1f;
        public GameObject player; //asign MC to this

        [SerializeField] private MentalHealth mentalHealth;
        public MentalHealthEffect mentalHealthEffect; //this is the mhEffect per world
        public GameObject vignetteDamage;
        public float fadeRate = 0.05f;

        // bool criticalLevel = false;
        float timer = 0;
        bool timerStart = false;

        #region Singleton
        public static PlayerMentalHealth instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Multiple player health managers found");
            }
            instance = this;
        }
        #endregion

        void Update()
        {
            if (mentalHealth != null)
                mentalHealth.SetSize(health);

            if (vignetteDamage != null && !timerStart)
            {
                SpriteRenderer vignette = vignetteDamage.GetComponent<SpriteRenderer>();
                Color vig = vignette.color;
                if (vig.a > 0)
                    vig.a -= fadeRate;
                else if (vig.a < 0)
                    vig.a = 0;

                vignette.color = vig;
            }

            if (timerStart)
            {
                timer += Time.deltaTime;
                if(timer % 60 >= 2)
                {
                    timerStart = false;
                    timer = 0;
                }
            }

            // if(criticalLevel) {
            //     timer += Time.deltaTime;
            //     if(timer % 60 >= 5) {
            //         resetLevel();
            //     }
            // }
        }

        void resetLevel()
        {
            changeHealth(1f);
            player.GetComponent<PlayerController>().reset();
            mentalHealthEffect.Stop();
        }

        public void changeHealth(float damageValue)
        {
            health += damageValue;

            if (damageValue < 0)
            {
                timerStart = false;
                timer = 0;
                SpriteRenderer vignette = vignetteDamage.GetComponent<SpriteRenderer>();
                //Color vig = new Color(0.1490196f, 0.05882353f, 0.227451f);
                Color vig = new Color(0.2220273f, 0f, 0.4150943f);
                vig.a = vignette.color.a;
                if (vig.a < 0.7)
                    vig.a += 0.35f;

                vignette.color = vig;
            } else if (damageValue > 0) {
                timerStart = true;
                timer = 0;
                SpriteRenderer vignette = vignetteDamage.GetComponent<SpriteRenderer>();
                Color vig = new Color(1f, 1f, 1f);
                vig.a = vignette.color.a;
                if (vig.a < 0.3)
                    vig.a += 0.15f;

                vignette.color = vig;
            }

            if (health >= 1)
            {
                health = 1;
                mentalHealthEffect.Stop();
            }

            if (health < 0f)
                health = 0f;

            if (health < 0.25f)
            {
                mentalHealthEffect.Trigger();
                // criticalLevel = true;
            }
            else
            {
                mentalHealthEffect.Stop();
                // criticalLevel = false;
            }

            if (health <= 0)
            {
                resetLevel();
            }
        }

        public void SwitchTarget(GameObject newPlayer)
        {
            player = newPlayer;
        }
    }
}