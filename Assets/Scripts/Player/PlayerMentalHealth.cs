﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Player
{
    public class PlayerMentalHealth : MonoBehaviour {

        public float health = 1f;
        public GameObject player;

        [SerializeField] private MentalHealth mentalHealth;
        [Header("Leave this blank.")]
        public MentalHealthEffect mentalHealthEffect;
        public MoodMentalHealth moodMentalHealth;
        public AnxietyMentalHealth anxietyMentalHealth;
        public ShizoMentalHealth schizoMentalHealth;
        
        Vector3 checkPoint;
        bool criticalLevel = false;
        float timer = 0;

        #region Singleton
        public static PlayerMentalHealth instance;

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("Multiple player health managers found");
            }
            instance = this;
        }
        #endregion

        void Update() {
            mentalHealth.SetSize(health);

            // if(criticalLevel) {
            //     timer += Time.deltaTime;
            //     if(timer % 60 >= 5) {
            //         resetLevel();
            //     }
            // }
        }

        void resetLevel() {
            changeHealth(1f);
            player.GetComponent<PlayerController>().respawn();
            player.transform.position = checkPoint;
        }

        public void SetLevel(string sceneName)
        {
            if(sceneName.CompareTo("Mood") == 0)
            {
                //Asign mood MH effect;
                mentalHealthEffect = moodMentalHealth;
                gameObject.GetComponent<MoodMentalHealth>().enabled = true;
                gameObject.GetComponent<AnxietyMentalHealth>().enabled = false;
                gameObject.GetComponent<ShizoMentalHealth>().enabled = false;
            } else if(sceneName.CompareTo("Anxiety") == 0) {
                //Asign anxiety MH effect;
                mentalHealthEffect = anxietyMentalHealth;
                gameObject.GetComponent<AnxietyMentalHealth>().enabled = true;
                gameObject.GetComponent<MoodMentalHealth>().enabled = false;
                gameObject.GetComponent<ShizoMentalHealth>().enabled = false;
            } else if(sceneName.CompareTo("Schizo") == 0) {
                //Asign schizo MH effect;
                mentalHealthEffect = schizoMentalHealth;
                gameObject.GetComponent<ShizoMentalHealth>().enabled = true;
                gameObject.GetComponent<MoodMentalHealth>().enabled = false;
                gameObject.GetComponent<AnxietyMentalHealth>().enabled = false;
            }
        }

        public void changeHealth(float damageValue) {
            health += damageValue;

            if(health >= 1) {
                health = 1;
                timer = 0;
            }
            
            if(health < 0f)
                health = 0f;

            if(health < 0.25f) {
                timer = 0;
                mentalHealthEffect.Trigger();
                // criticalLevel = true;
            }
            else {
                mentalHealthEffect.Stop();
                // criticalLevel = false;
            }

            if(health <= 0) {
                resetLevel();
            }
        }

        public void SwitchTarget(GameObject newPlayer)
        {
            player = newPlayer;
        }
    }
}