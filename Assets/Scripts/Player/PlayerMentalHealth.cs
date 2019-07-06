using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerMentalHealth : MonoBehaviour
    {

        public float health = 1f;
        public GameObject player;

        [SerializeField] private MentalHealth mentalHealth;
        [Header("Leave this blank.")]
        public MentalHealthEffect mentalHealthEffect;
        public MoodMentalHealth moodMentalHealth;
        public AnxietyMentalHealth anxietyMentalHealth;
        public ShizoMentalHealth schizoMentalHealth;
        public GameObject vignetteDamage;
        public float fadeRate = 0.05f;

        Vector3 checkPoint;
        bool criticalLevel = false;
        float timer = 0;

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

            if (vignetteDamage != null)
            {
                SpriteRenderer vignette = vignetteDamage.GetComponent<SpriteRenderer>();
                Color vig = vignette.color;
                if (vig.a > 0)
                    vig.a -= fadeRate;
                else if (vig.a < 0)
                    vig.a = 0;

                vignette.color = vig;
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
            player.GetComponent<PlayerController>().respawn();
            player.transform.position = checkPoint;
        }

        public void SetLevel(string sceneName)
        {
            player = GameObject.Find("mc");

            if (sceneName.CompareTo("Mood") == 0)
            {
                //Asign mood MH effect;
                Debug.Log("Set Mood MH");
                mentalHealthEffect = moodMentalHealth;
                gameObject.GetComponent<MoodMentalHealth>().enabled = true;
                gameObject.GetComponent<AnxietyMentalHealth>().enabled = false;
                gameObject.GetComponent<ShizoMentalHealth>().enabled = false;
            }
            else if (sceneName.CompareTo("Anxiety") == 0)
            {
                //Asign anxiety MH effect;
                Debug.Log("Set Anxiety MH");
                mentalHealthEffect = anxietyMentalHealth;
                gameObject.GetComponent<AnxietyMentalHealth>().enabled = true;
                gameObject.GetComponent<MoodMentalHealth>().enabled = false;
                gameObject.GetComponent<ShizoMentalHealth>().enabled = false;
            }
            else if (sceneName.CompareTo("Schizo") == 0)
            {
                //Asign schizo MH effect;
                Debug.Log("Set Schizo MH");
                mentalHealthEffect = schizoMentalHealth;
                gameObject.GetComponent<ShizoMentalHealth>().enabled = true;
                gameObject.GetComponent<MoodMentalHealth>().enabled = false;
                gameObject.GetComponent<AnxietyMentalHealth>().enabled = false;
            }
        }

        public void changeHealth(float damageValue)
        {
            health += damageValue;

            if (damageValue < 0)
            {
                SpriteRenderer vignette = vignetteDamage.GetComponent<SpriteRenderer>();
                Color vig = new Color(0.1490196f, 0.05882353f, 0.227451f);
                vig.a = vignette.color.a;
                if (vig.a < 1)
                    vig.a += 0.5f;

                vignette.color = vig;
            } else if (damageValue > 0) {
                SpriteRenderer vignette = vignetteDamage.GetComponent<SpriteRenderer>();
                Color vig = new Color(0.5946885f, 1f, 0.1686275f);
                vig.a = vignette.color.a;
                if (vig.a < 1)
                    vig.a += 0.5f;

                vignette.color = vig;
            }

            if (health >= 1)
            {
                health = 1;
                timer = 0;
            }

            if (health < 0f)
                health = 0f;

            if (health < 0.25f)
            {
                timer = 0;
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