using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using UI;

namespace GameManager
{
    public class SceneController : MonoBehaviour
    {
        Scene scene;

        #region Singleton
        public static SceneController instance;

        void Awake()
        {
            if(instance != null)
            {
                Debug.LogWarning("Multiple scene controllers switches found");
            }
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        #endregion

        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            scene = SceneManager.GetActiveScene();
            AssignSceneValues();
        }

        public string GetSceneName()
        {
            return scene.name;
        }

        public void LoadNewScene()
        {
            if(scene.name.CompareTo("Mood") == 0)
            {
                SceneManager.LoadScene("Anxiety");
            } else if(scene.name.CompareTo("Anxiety") == 0) {
                SceneManager.LoadScene("Schizo");
            } else if(scene.name.CompareTo("Schizo") == 0) {
                Debug.Log("EndGame");
            }
        }

        private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
        {
            scene = SceneManager.GetActiveScene();
            AssignSceneValues();
        }

        void AssignSceneValues()
        {
            PlayerMentalHealth.instance.SetLevel(scene.name);
            Debug.Log("Active scene = " + scene.name);
            //Assign NPC to PlayerSwitch
            CameraFollow.instance.SetLevel();
            UIController.instance.SetLevel();
            PlayerSwitch.instance.SetLevel();
        }
    }
}