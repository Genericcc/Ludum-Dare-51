using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animManager;

    public Vector2 movementInput;
    public float moveAmount;
    public Vector2 cameraInput;
    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Awake()
    {
        animManager = GetComponent<AnimatorManager>();
    }

    public void AllInputs()
    {
        HandleMovementInput();
        HandleActions();
    }
    
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + MathF.Abs(verticalInput));
        animManager.UpdateAnimatorValues(moveAmount, 0);
    }

    private void HandleActions()
    {
      
        if(Input.GetMouseButton(0))
        {
            Actions.Instance.Attack();
        }
    
        if(Input.GetMouseButton(1))
        {
            Actions.Instance.Defend();
        }
    }
    
}
