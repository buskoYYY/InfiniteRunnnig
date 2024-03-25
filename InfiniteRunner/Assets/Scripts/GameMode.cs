using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMode : MonoBehaviour
{
    
    public delegate void OnGameOver();

    [Header("Settings")]
    [SerializeField] int mainMenuSceneBuildIndex;
    [SerializeField] int firsSceneIndex;

    public event OnGameOver onGameOver;

    [Header("Elements")]
    private bool isGameOver = false;
    public void GameOver()
    {
        SetGamePaused(true);
        UpdateLeaderBoard();

        isGameOver = true;
        onGameOver?.Invoke();
    }

    private void UpdateLeaderBoard()
    {
        int score = GetComponent<ScoreKeeper>().Score;
        SaveDataManager.SaveNewLeaderBoardEntry("TestPlAYER", DateTime.Now, score);
    }

    public void SetGamePaused(bool isPaused)
    {
        if(isGameOver) { return; }
        
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
        LoadScene(mainMenuSceneBuildIndex);
    }

    private void LoadScene(int sceneBuidIndex)
    {
        isGameOver = false;
        SetGamePaused(false);
        SceneManager.LoadScene(sceneBuidIndex);
    }

    internal void RestartCurrentLevel()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    internal bool IsGameOver()
    {
        return isGameOver;
    }

    internal void LoadFirstLevel()
    {
        LoadScene(firsSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
