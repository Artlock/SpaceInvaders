using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static Loader Instance { get; private set; }

    public string gameSceneName = "Game";

    private void Awake()
    {
        if (Instance != null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            return;
    }

    public void Reload()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
