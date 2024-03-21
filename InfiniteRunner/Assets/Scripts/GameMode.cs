using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
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
}
