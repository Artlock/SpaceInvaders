using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float reloadGameDelay = 10f;

    public static event Action OnEndOfGame;
    public static event Action OnGameOver;
    public static event Action OnVictory;

    private bool gameEnded = false;
    private Coroutine endOfGame;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        } 
        else
        {
            Instance = this;
        }
    }

    public void EndGame(bool victory)
    {
        if (gameEnded) return; // Dont end twice

        gameEnded = true;

        OnEndOfGame?.Invoke();

        if (victory) OnVictory?.Invoke();
        else OnGameOver?.Invoke();

        TimeManager.Instance.ChangeTimescale(0f);

        if (endOfGame != null)
        {
            StopCoroutine(endOfGame);
            endOfGame = null;
        }
        endOfGame = StartCoroutine(ResetGame());
    }

    private IEnumerator ResetGame()
    {
        yield return new WaitForSecondsRealtime(reloadGameDelay);

        Loader.Instance.Reload();
    }
}
