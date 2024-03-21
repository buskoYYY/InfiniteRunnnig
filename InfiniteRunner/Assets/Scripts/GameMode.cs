using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int mainMenuSceneBuildIndex;
    public void GameOver()
    {
        SetGamePaused(true);
    }

    public void SetGamePaused(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    internal void TogglePause()
    {
        if (IsGamePaused())
        {
            SetGamePaused(false);
        }
        else
        {
            SetGamePaused(true);
        }

    }

    public bool IsGamePaused()
    {
        return Time.timeScale == 0;
    }

    public void BackToMainMenu()
    {
        GoToScene(mainMenuSceneBuildIndex);
    }

    private void GoToScene(int sceneBuidIndex)
    {
        SetGamePaused(false);
        SceneManager.LoadScene(sceneBuidIndex);
    }

    internal void RestartCurrentLevel()
    {
        GoToScene(SceneManager.GetActiveScene().buildIndex);
    }
}
