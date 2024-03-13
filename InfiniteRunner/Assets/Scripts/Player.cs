using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Elements")]
    PlayerInput playerInput; // ������ � ������� ��������� ��������� ���� ������


    [Header("Settings")]
    [SerializeField] Transform[] laneTransform;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float jumpHight = 2.5f;
    private Vector3 destination;
    private int currentIndex;

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput(); // ������� ��������� ������
        }
        playerInput.Enable(); // ������ ��� ���������
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    void Start()
    {
        // �������� �� ������� ������� �������
        playerInput.gamePlay.Move.performed += MovePerformed; // ��� ����������� ������� ������� ����������� ����� ���������� �� ���������
                                                              // playerInput - ��������� ������
                                                              // gamePlay - ���� actions ������� �� �������
                                                              // Move - action � ����� GamePlay
                                                              // performed - ���� ������ ������
                                                              // canceled - ���� ������ ��������
                                                              //  ������� (event)
        playerInput.gamePlay.Jump.performed += JumpPerformed;

        for (int i = 0; i < laneTransform.Length; i++)
        {
            if (laneTransform[i].position == transform.position)
            {
                currentIndex = i;
                destination = laneTransform[i].position;
            }
        }

    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();

        if (rigidBody != null)
        {
            float jumpUpSpeed = Mathf.Sqrt(2 * jumpHight * Physics.gravity.magnitude);
            rigidBody.AddForce(new Vector3(0, jumpUpSpeed, 0), ForceMode.VelocityChange);
        }
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
       if(!IsOnGround()) { return; }

        float inputValue = context.ReadValue<float>(); // ����� ��������� -1, 1

        if (inputValue > 0f)
        {
            MoveRight();
        }

        else if (inputValue < 0f)
        {
            MoveLeft();
        }
    }

    private void MoveLeft()
    {
        if (currentIndex == 0) { return; }

        currentIndex--;
        destination = laneTransform[currentIndex].position;
    }

    private void MoveRight()
    {
        if (currentIndex == laneTransform.Length - 1) { return; }

        currentIndex++;
        destination = laneTransform[currentIndex].position;
    }

    void Update()
    {
        float transformX = Mathf.Lerp(transform.position.x, destination.x, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(transformX, transform.position.y, transform.position.z); // �������� �� �������� y � z ����� �������� ������
    }

    private bool IsOnGround()
    {
        return true;
    }
}
