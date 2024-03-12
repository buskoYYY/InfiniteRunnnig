using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInput playerInput; // скрипт в котором хран€тьс€ созданыые нами экшены

    private void OnEnable()
    {
        if (playerInput == null)
        {
            playerInput = new PlayerInput(); // создаем экземпл€р класса
        }
        playerInput.Enable(); // делаем его доступным
    }

    private void OnDisable()
    {
        playerInput.Disable(); 
    }
    void Start()
    {
        playerInput.gamePlay.Move.performed += MovePerformed;
        // playerInput - экземпл€р класса
        // gamePlay - лист actions который мы создали
       // Move - action в листе GamePlay
       //  событие (event)

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
