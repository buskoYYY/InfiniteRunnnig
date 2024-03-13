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
       for(int i = 0; i < laneTransform.Length; i++)
        {
            if (laneTransform[i].position == transform.position)
            {
                currentIndex = i;
                destination = laneTransform[i].position;
            }
        }

    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
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
        if(currentIndex == 0) { return; }

        currentIndex--;
        destination = laneTransform[currentIndex].position;
    }

    private void MoveRight()
    {
        if(currentIndex == laneTransform.Length - 1) { return; }

        currentIndex++;
        destination = laneTransform[currentIndex].position;
    }

    void Update()
    {
        transform.position = destination;   
    }
}
