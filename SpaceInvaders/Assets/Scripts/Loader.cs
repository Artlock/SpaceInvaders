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
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        Reload(); // Initial game loading
    }

    public void Reload()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }
}
