using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public delegate void OnGlobalSpeedChanged(float newSpeed);
    [SerializeField] private float globalSpeed;

    public event OnGlobalSpeedChanged onGlobalSpeedChanged;

    public void ChangeGlobalSpeed(float speedChange, float duration)
    {
        globalSpeed += speedChange;
        InformSpeedChange();
        StartCoroutine(RemoveSpeedChange(speedChange, duration));

    }

    public float GetGlobalSpeed()
    {
        return globalSpeed;
    }

    IEnumerator RemoveSpeedChange(float speedChangeAmount, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        globalSpeed -= speedChangeAmount;
        InformSpeedChange();
    }

    private void InformSpeedChange()
    {
        onGlobalSpeedChanged?.Invoke(globalSpeed);
    }
}
