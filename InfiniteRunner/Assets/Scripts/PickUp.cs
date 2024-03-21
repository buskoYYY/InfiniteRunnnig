using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Spawnable
{
    [Header("Settings")]
    [SerializeField] private int scoreEffect;
    [SerializeField] private float speedEffect;
    [SerializeField] private float speedEffectDuration;
    bool isAdjacted = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpeedController speedController = FindObjectOfType<SpeedController>();
            if (speedController != null && speedEffect != 0)
            {
                speedController.ChangeGlobalSpeed(speedEffect, speedEffectDuration);
            }

            ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
            if (scoreKeeper != null && scoreEffect != 0)
            {
                scoreKeeper.ChangeScore(scoreEffect);
            }
            PickUpBy(other.gameObject);
        }

        if (other.gameObject.tag == "Threat" && !isAdjacted)
        {
            Collider col = other.GetComponent<Collider>();
            if (col != null)
            {
                transform.position = col.bounds.center + (col.bounds.extents.y + gameObject.GetComponent<Collider>().bounds.center.y) * Vector3.up;
                isAdjacted = true;
            }
        }
    }

    protected virtual void PickUpBy(GameObject picker)
    {
        Destroy(gameObject);
    }
}
