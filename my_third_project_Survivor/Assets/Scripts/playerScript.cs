using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerScript : MonoBehaviour
{
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;

    private Vector2 charMovementInput;
    private Vector3 CharMove;
    private bool isMoving;

    [SerializeField] private float vel;
    private float rotationVelocity;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        animator = GetComponent<Animator>();

        playerInput.movement.walk.started += OnMovementInput;

        playerInput.movement.walk.canceled += OnMovementInput;

        playerInput.movement.walk.performed += OnMovementInput;
        



    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        charMovementInput = context.ReadValue<Vector2>();
        CharMove.x = charMovementInput.x;
        CharMove.y = 0f;
        CharMove.z = charMovementInput.y;
        isMoving = charMovementInput.y != 0 || charMovementInput.x != 0;
    }
    
    void Update()
    {
        MovePlayer();
        AnimationHandler();
        PlayerRotationHandler();
    }

    private void PlayerRotationHandler()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = CharMove.x;
        positionToLookAt.y = 0f;    
        positionToLookAt.z = CharMove.z;
        Quaternion currentRotation = transform.rotation;


        if (isMoving)
        {
            Quaternion lookAtRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, lookAtRotation, rotationVelocity * Time.deltaTime);
        }

    }

    private void AnimationHandler()
    {
        if (!animator.GetBool("isWalking") && isMoving)
        {
            animator.SetBool("isWalking", true);
        } 
        if (animator.GetBool("isWalking") && !isMoving)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void MovePlayer()
    {
        characterController.Move(CharMove * Time.deltaTime * vel);
    }

    private void OnEnable()
    {
        playerInput.movement.Enable();
    }

    private void OnDisable()
    {
        playerInput.movement.Disable();
    }
}
