using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [Header ("Elements")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] UISwitcher menuSwitcher;
    [SerializeField] Transform gameUI;
    [SerializeField] Transform pauseUI;
    [SerializeField] Transform gameOverUI;
    void Start()
    {
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if(scoreKeeper != null )
        {
            scoreKeeper.onScoreChanged += UpdateScoreText;
        }
    }

    private void UpdateScoreText(int newVal)
    {
        scoreText.SetText("Score: " + newVal);
    }

    internal void SignalPause(bool isGamePaused)
    {
        if(isGamePaused)
        {
            menuSwitcher.SetActiveUI(pauseUI);
        }
        else
        {
            menuSwitcher.SetActiveUI(gameUI);
        }
    }
}
