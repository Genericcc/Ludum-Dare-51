using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*questions: should I make separate classes for the Animator?*/
public class Movement : MonoBehaviour
{
    
    Transform cameraObject;
    InputManager inputManager;
    Vector3 moveDirection;

    [HideInInspector]
    public AnimatorManager animatorHandler;


    public float runSpeed = 6f;
    public float RotationSpeed = 15;
    public int jumpForce;



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



    public void AllMovement()
    {
        HandleMovement();
        HandleRotation();

    }
    #region Movement

    private void HandleMovement()
    {

        //Vector3 moveDirection = Vector3.zero;
        

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * runSpeed;

        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
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
