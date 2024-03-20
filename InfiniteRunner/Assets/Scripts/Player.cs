using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Elements")]
    PlayerInput playerInput; // ������ � ������� ��������� ��������� ���� ������
    [SerializeField] Transform[] laneTransform;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask groundCheckMask;
    private Animator playerAnimator;
    private Camera playerCamera;


    [Header("Settings")]
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float jumpHight = 2.5f;
    [SerializeField][Range(0, 1)] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 blockageCheckHalfExtented;
    [SerializeField] string blockageCheckTag = "Threat";
    private Vector3 destination;
    private Vector3 playerCameraOffset;
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

        playerAnimator = GetComponent<Animator>();

        playerCamera = Camera.main;
        playerCameraOffset = playerCamera.transform.position - transform.position;
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

        float inputValue = context.ReadValue<float>(); // ����� ��������� -1, 1
        int goalIndex = currentIndex;
        if (inputValue > 0f)
        {
            if (goalIndex == laneTransform.Length - 1) { return; }
            goalIndex++;
        }

        else if (inputValue < 0f)
        {
            if (currentIndex == 0) { return; }
            goalIndex--;
        }

        Vector3 goalPos = laneTransform[goalIndex].position;
        if (GamePlayStatic.IsPositionOccupied(goalPos, blockageCheckHalfExtented, blockageCheckTag))
        {
            return;
        }
        currentIndex = goalIndex;
        destination = goalPos;
    }

    void Update()
    {
        if (!IsOnGround())
        {
            playerAnimator.SetBool("isOnGround", false);
        }
        else
        {
            playerAnimator.SetBool("isOnGround", true);
        }

        float transformX = Mathf.Lerp(transform.position.x, destination.x, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(transformX, transform.position.y, transform.position.z); // �������� �� �������� y � z ����� �������� ������   
    }
    private void LateUpdate()
    {
        playerCamera.transform.position = transform.position + playerCameraOffset;
    }
    private bool IsOnGround()
    {
        return Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundCheckMask);
    }
}
