using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Elements")]
    PlayerInput playerInput; // скрипт в котором хран€тьс€ созданыые нами экшены


    [Header("Settings")]
    [SerializeField] Transform[] laneTransform;
    private Vector3 destination;
    private int currentIndex;

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
        // подписка на событие нажати€ клавиши
        playerInput.gamePlay.Move.performed += MovePerformed; // при наступлени€ данного событи€ запускаетс€ метод подход€щий по сигнатуре
        // playerInput - экземпл€р класса
        // gamePlay - лист actions который мы создали
       // Move - action в листе GamePlay
       // performed - если кнопка нажата
       // canceled - если кнопка отпущена
       //  событие (event)
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
        float inputValue = context.ReadValue<float>(); // будет равн€тьс€ -1, 1
        
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
