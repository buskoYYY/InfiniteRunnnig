using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComp : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] Vector3 moveDirection = Vector3.forward;
    [SerializeField] Vector3 destination;

    private void Start()
    {
        SpeedController speedController = FindObjectOfType<SpeedController>();
        if (speedController != null )
        {
            speedController.onGlobalSpeedChanged += SetMoveSpeed;
            SetMoveSpeed(speedController.GetGlobalSpeed());
        }
    }

    public void SetMoveDir(Vector3 dir)
    {
        moveDirection = dir;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if(Vector3.Dot(destination-transform.position, moveDirection) < 0)
        {
            Destroy(gameObject);
        }
    }
}
