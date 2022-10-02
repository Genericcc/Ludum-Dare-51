using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public static event EventHandler OnStartedAttacking;
    public static event EventHandler OnStartedDefending;
    public static event EventHandler OnFinishedDefending;

    PlayerControls playerControls;
    AnimatorManager animManager;

    public Vector2 movementInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    private void Awake()
    {
        if(Instance != null) 
        {
            Debug.LogError("There's more than one Actions! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        animManager = GetComponent<AnimatorManager>();
    }

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

    public void HandleActions()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        { 
            OnStartedAttacking?.Invoke(this, EventArgs.Empty);
        }    

        if(Input.GetKeyDown(KeyCode.Mouse1))
        { 
            OnStartedDefending?.Invoke(this, EventArgs.Empty);
        }    

        if(Input.GetKeyUp(KeyCode.Mouse1))
        { 
            OnFinishedDefending?.Invoke(this, EventArgs.Empty);
        }    
    }

    public bool IsMouseRightPressed()
    {
        if(Input.GetMouseButton(1))
        {
            return true;
        }
        else
        {
            return false;
        } 
    }
}
