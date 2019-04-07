using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;

public class SceneController : MonoBehaviour
{
    Scene scene;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        AssignSceneValues();
    }

    public string GetSceneName()
    {
        return scene.name;
    }

    void AssignSceneValues()
    {
        PlayerMentalHealth.instance.SetLevel(scene.name);

        //Assign NPC to PlayerSwitch
        PlayerSwitch.instance.SetLevel();
    }
}
