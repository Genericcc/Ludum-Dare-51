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
    // public float verticalInput;
    // public float horizontalInput;

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

    private void Start() 
    {
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;     
    }

    private void PlayerHealth_OnPlayerDeath(object sender, EventArgs e)
    {
        this.enabled = false;
    }

    // private void OnEnable()
    // {
    //     if (playerControls == null)
    //     {
    //         playerControls = new PlayerControls();
    //         playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
    //     }
    //     playerControls.Enable();
    // }

    // private void OnDisable()
    // {
    //     playerControls.Disable();
    // }

    private void Update()
    {
        HandleMouse();
    }

    public void HandleMouse()
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

    public Vector3 GetMoveVector()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        if(Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }   
        if(Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        } 
        if(Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        } 
        if(Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }  

        return inputMoveDir;
    }

    public float GetCameraRotateAmount()
    {
        float rotateAmount = 0f;

        if(Input.GetKey(KeyCode.Q))
        {
            rotateAmount = +1f;
           // Debug.Log(rotateAmount);
        }

        if(Input.GetKey(KeyCode.E))
        {
            rotateAmount = -1f;
            //Debug.Log(rotateAmount);
        }

        return rotateAmount;
    }

    public float GetCameraZoomAnout()
    {
        float zoomAmount = 0f;

        if(Input.mouseScrollDelta.y > 0)
        {
            zoomAmount = -1f;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            zoomAmount = +1f;
        }

        return zoomAmount;
    }

}
