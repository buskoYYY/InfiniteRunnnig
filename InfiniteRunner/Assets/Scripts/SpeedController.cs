using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public delegate void OnGlobalSpeedChanged(float newSpeed);
    [SerializeField] private float globalSpeed;

    public event OnGlobalSpeedChanged onGlobalSpeedChanged;

    public void SetGlobalSpeed(float newSpeed)
    {
        globalSpeed = newSpeed;
        onGlobalSpeedChanged?.Invoke(globalSpeed);
    }

    public float GetGlobalSpeed()
    {
        return globalSpeed;
    }
}
