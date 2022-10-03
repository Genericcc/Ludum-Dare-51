using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*questions: should I make separate classes for the Animator?*/
public class Movement : MonoBehaviour
{
    [SerializeField] float downpull;
    [SerializeField] LayerMask groudLayerMask;

    Transform cameraObject;
    InputManager inputManager;
    Vector3 moveDirection;

    [HideInInspector]
    public AnimatorManager animatorHandler;

    public float runSpeed = 6f;
    public float RotationSpeed = 15;
    public int jumpForce;
    public bool isActive;

    CapsuleCollider coll;
    Rigidbody rb;
    Animator anim;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        animatorHandler = GetComponentInChildren<AnimatorManager>();
    }

    private void Start() 
    {
        InputManager.OnStartedAttacking += InputManager_OnStartedAttacking;
        InputManager.OnStartedDefending += InputManager_OnStartedDefending; 
        InputManager.OnFinishedDefending += InputManager_OnFinishedDefending; 
    }

    private void InputManager_OnStartedAttacking(object sender, EventArgs e)
    {
        isActive = true;
    }

    private void InputManager_OnStartedDefending(object sender, EventArgs e)
    {
        isActive = true;
    }

    private void InputManager_OnFinishedDefending(object sender, EventArgs e)
    {
        isActive = false;
    }

    private void OnFinishedAttacking()
    {
        isActive = false;
    }

    private void Update() 
    {
        Gravity();
    }

    private void Gravity()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groudLayerMask))
        {
            if(Vector3.Distance(transform.position, hit.point) > 0.1f)
            {
                transform.position += Vector3.down * 0.1f * downpull;
            }
        }
    }

    public void AllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    #region Movement

    private void HandleMovement()
    {
        if(!isActive)
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection += cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
            moveDirection = moveDirection * runSpeed;

            Vector3 movementVelocity = moveDirection;
            rb.velocity = movementVelocity;

            if(movementVelocity != new Vector3(0,0,0))
            {
                animatorHandler.StartWalking(true);
            }
            else
            {
                animatorHandler.StartWalking(false);
            }
        }
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }










    /*

    private void Run()
    {
        //Animator and transform goes here.
    }


    private void Jump()
    {
        //Jump function. Duh.


    }*/
    #endregion
}
