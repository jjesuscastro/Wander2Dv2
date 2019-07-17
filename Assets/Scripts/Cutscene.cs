using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Player;
using GameManager;

namespace GameManager
{
    public class Cutscene : MonoBehaviour
    {
        public UnityEvent endScene;
        public SpriteRenderer blackCover;
        public SpriteRenderer sceneRenderer;
        public SpriteRenderer blankSceneRenderer;
        public Sprite[] cutscenes;
        public AudioClip newClip;
        AudioSource bgm;

        public int currScene = 0;
        float timer = 0;
        float sceneTimer = 0;
        bool sceneStarting = false;
        bool startTimer = false;
        bool scenePlaying = false;
        bool sceneChanging = false;
        bool fadeMusic = false;
        bool changeMusic = false;
        public GameObject cutsceneRenderer;
        public int stopMusicOnScene = 0;

        #region Singleton
            public static Cutscene instance;

            void Awake()
            {
                if (instance != null)
                {
                    Debug.LogWarning("Multiple cutscenes found");
                }
                instance = this;
            }
        #endregion

        void Update()
        {
            if(bgm == null)
            {
                bgm = Camera.main.gameObject.GetComponent<AudioSource>();
            }

            if(fadeMusic)
            {
                bgm.volume -= 0.01f;
                if(bgm.volume <= 0)
                {
                    bgm.volume = 0;
                    fadeMusic = false;
                    if(newClip != null)
                    {
                        bgm.clip = newClip;
                        changeMusic = true;
                    }
                }
            }

            if(changeMusic)
            {
                bgm.volume += 0.01f;
                if(bgm.volume >= 1)
                {
                    bgm.volume = 1;
                    changeMusic = false;
                }
            }

            if (sceneStarting && !scenePlaying)
            {
                Color color = blackCover.color;
                color.a += 0.025f;
                blackCover.color = color;
                timer += Time.deltaTime;
                startTimer = true;
            }

            if (startTimer && !scenePlaying)
            {
                if (timer % 60 > 1)
                {
                    cutsceneRenderer.SetActive(true);
                    NextScene();
                    sceneStarting = false;
                    timer = 0;
                    startTimer = false;
                }
            }

            if (!sceneStarting)
            {
                Color color = blackCover.color;
                color.a -= 0.025f;
                if (color.a >= 0)
                    blackCover.color = color;
            }

            if (scenePlaying && !sceneChanging)
            {
                Color color = blankSceneRenderer.color;
                color.a -= 0.025f;
                if (color.a >= 0)
                    blankSceneRenderer.color = color;

                sceneTimer += Time.deltaTime;
                if (sceneTimer % 60 > 5)
                {
                    sceneTimer = 0;
                    sceneChanging = true;
                }
            }
            //test
            if (scenePlaying && sceneChanging)
            {
                Color color = blankSceneRenderer.color;
                color.a += 0.025f;
                blankSceneRenderer.color = color;

                if (color.a <= 1)
                    blankSceneRenderer.color = color;

                sceneTimer += Time.deltaTime;
                if (sceneTimer % 60 > 0.6)
                {
                    sceneTimer = 0;
                    NextScene();
                    sceneChanging = false;
                }
            }
        }

        public void StartScene()
        {
            Debug.Log("[Cutscene.cs] - Starting cutscene.");
            PlayerMovement[] players = GameObject.FindObjectsOfType<PlayerMovement>();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].enabled = false;
            }

            Color color = blackCover.color;
            color.a = 0;
            blackCover.color = color;
            sceneStarting = true;
        }

        public void NextScene()
        {
            if (currScene != cutscenes.Length)
            {
                Debug.Log("[Cutscene.cs] - Playing scene " + currScene);
                sceneRenderer.sprite = cutscenes[currScene];
                currScene++;
                scenePlaying = true;

                if(currScene == stopMusicOnScene)
                {
                    fadeMusic = true;
                    changeMusic = false;
                }
            }
            else
            {
                Debug.Log("[Cutscene.cs] - Cutscene done. Loading next scene.");
                scenePlaying = false;
                SceneController.instance.LoadNewScene();
            }
        }
    }
}