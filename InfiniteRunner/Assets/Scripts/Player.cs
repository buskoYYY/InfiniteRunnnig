using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Elements")]
    PlayerInput playerInput; // скрипт в котором хран€тьс€ созданыые нами экшены
    [SerializeField] Transform[] laneTransform;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask groundCheckMask;
    private Animator playerAnimator;


    [Header("Settings")]
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float jumpHight = 2.5f;
    [SerializeField][Range(0, 1)] float groundCheckRadius = 0.2f;
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
        playerInput.gamePlay.Jump.performed += JumpPerformed;

        for (int i = 0; i < laneTransform.Length; i++)
        {
            if (laneTransform[i].position == transform.position)
            {
                currentIndex = i;
                destination = laneTransform[i].position;
            }
        }

        playerAnimator = GetComponent<Animator>();
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        if (IsOnGround())
        {
            Rigidbody rigidBody = GetComponent<Rigidbody>();

            if (rigidBody != null)
            {
                float jumpUpSpeed = Mathf.Sqrt(2 * jumpHight * Physics.gravity.magnitude);
                rigidBody.AddForce(new Vector3(0, jumpUpSpeed, 0), ForceMode.VelocityChange);
            }
        }


    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        if (!IsOnGround()) { return; }

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
        if (!IsOnGround())
        {
            playerAnimator.SetBool("isOnGround", false);
            return;
        }
        playerAnimator.SetBool("isOnGround", true);

        float transformX = Mathf.Lerp(transform.position.x, destination.x, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(transformX, transform.position.y, transform.position.z); // движение по позици€м y и z будет согласно физике
    }

    private bool IsOnGround()
    {
        Vector3 vec1 = laneTransform[0].transform.position - laneTransform[2].transform.position;
        Debug.Log(vec1.magnitude);
        return Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundCheckMask);
    }
}
