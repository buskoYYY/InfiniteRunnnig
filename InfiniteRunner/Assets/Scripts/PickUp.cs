using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Spawnable
{
    [Header("Settings")]
    [SerializeField] private int scoreEffect;
    [SerializeField] private float speedEffect;
    [SerializeField] private float speedEffectDuration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SpeedController speedController = FindObjectOfType<SpeedController>();
            if(speedController != null && speedEffect != 0)
            {
                speedController.ChangeGlobalSpeed(speedEffect, speedEffectDuration);
            }

            ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
            if (scoreKeeper != null && scoreEffect != 0)
            {
                scoreKeeper.ChangeScore(scoreEffect);
            }
        Destroy(gameObject);
        }
    }
}
