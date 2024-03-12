using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput; // ������ � ������� ��������� ��������� ���� ������

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
        playerInput.gamePlay.Move.performed += MovePerformed;
        // playerInput - ��������� ������
        // gamePlay - ���� actions ������� �� �������
       // Move - action � ����� GamePlay
       //  ������� (event)

    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        float inputValue = context.ReadValue<float>();
        Debug.Log(inputValue);
    }

    void Update()
    {
        
    }
}
