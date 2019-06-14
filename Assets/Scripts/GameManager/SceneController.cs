using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Player;
using UI;

namespace GameManager
{
    public class SceneController : MonoBehaviour
    {
        Scene scene;
        public GameObject loadScreen;
        public Slider slider;

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
            
            if(scene.name.CompareTo("MainMenu") != 0)
            {
                AssignSceneValues();
            }
        }

        public string GetSceneName()
        {
            return scene.name;
        }

        public void LoadNewScene()
        {
            if(scene.name.CompareTo("MainMenu") == 0)
            {
                StartCoroutine(LoadAsync("Mood"));
            } else if(scene.name.CompareTo("Mood") == 0) {
                StartCoroutine(LoadAsync("Anxiety"));
            } else if(scene.name.CompareTo("Anxiety") == 0) {
                StartCoroutine(LoadAsync("Schizo"));
            } else if(scene.name.CompareTo("Schizo") == 0) {
                Debug.Log("EndGame");
            }
        }

        IEnumerator LoadAsync(string sceneName)
        {
            Debug.Log("Loading New Scene");
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            loadScreen.gameObject.SetActive(true);

            while(!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;

                yield return null;
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