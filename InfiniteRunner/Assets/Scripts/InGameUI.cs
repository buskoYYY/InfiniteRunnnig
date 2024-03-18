using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [Header ("Elements")]
    [SerializeField] TextMeshProUGUI scoreText;
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

    void Update()
    {
        
    }
}
