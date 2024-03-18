using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] MovementComp movementComp;

    [Header("Settings")]
    [SerializeField] float spawnInterval = 2.0f;

    public float SpawnInterval
    {
        get { return spawnInterval; }
    }

    public MovementComp GetMovementComponent()
    {
        return movementComp;
    }
}
