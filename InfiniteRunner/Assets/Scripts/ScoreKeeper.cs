using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public delegate void OnScoreChanged(int newVal);
    public event OnScoreChanged onScoreChanged;

    [Header("Settings")]
    private int score;

    public void ChangeScore(int amt)
    {
        score += amt;
        if (score < 0) { score = 0; }
        onScoreChanged?.Invoke(score);
    }
}
