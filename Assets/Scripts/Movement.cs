using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


/*questions: should I make separate classes for the Animator?*/
public class Movement : MonoBehaviour
{
    public static event EventHandler OnMovementStarted;
    public static event EventHandler OnMovementFinished;

     // [SerializeField] private LayerMask groudLayerMask;
    // [SerializeField] private float downpull;
    [SerializeField] private CinemachineVirtualCamera testCamera;


    [HideInInspector]
    public AnimatorManager animatorHandler;
  
    [SerializeField] private float toTargetRotation = 360f;
    [SerializeField] private float runSpeed = 7;
    [SerializeField] private bool isActive;
    [SerializeField] private float cameraRotation;

    private Rigidbody rb;
    private Vector3 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        animatorHandler = GetComponentInChildren<AnimatorManager>();
    }

    private void Start() 
    {
        Actions.OnStartedAttacking += Actions_OnStartedAttacking;
        Actions.OnFinishedAttacking += Actions_OnFinishedAttacking;

        InputManager.OnStartedDefending += InputManager_OnStartedDefending; 
        InputManager.OnFinishedDefending += InputManager_OnFinishedDefending; 
    }

    //Events
    #region 

    private void Actions_OnStartedAttacking(object sender, EventArgs e)
    {
        isActive = true;
    }

    private void Actions_OnFinishedAttacking(object sender, EventArgs e)
    {
        isActive = false;
    }

    private void InputManager_OnStartedDefending(object sender, EventArgs e)
    {
        isActive = true;
    }

    private void InputManager_OnFinishedDefending(object sender, EventArgs e)
    {
        isActive = false;
    }

    #endregion

    private void Update() 
    {
        GatherInput();
        Rotate();
    }

    private void FixedUpdate() 
    {
        if(input == Vector3.zero || isActive)
        {
            OnMovementFinished?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            OnMovementStarted?.Invoke(this, EventArgs.Empty);

            Move();
            
            //transform.position += moveDirection * Time.deltaTime * runSpeed;
        }
    }

    private void GatherInput()
    {
        input = InputManager.Instance.GetMoveVector(); //new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        cameraRotation = testCamera.transform.eulerAngles.y;
    }

    private void Rotate()
    {
        if(input == Vector3.zero) { return; }

        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, cameraRotation, 0));
        input = isoMatrix.MultiplyPoint3x4(input);

        var rot = Quaternion.LookRotation(input, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, toTargetRotation * Time.deltaTime);

        //Rotation
        // float xShift = InputManager.Instance.GetMoveVector().x * runSpeed * Time.deltaTime;
        // float zShift = InputManager.Instance.GetMoveVector().z * runSpeed * Time.deltaTime;

        // moveDirection.x = xShift;
        // moveDirection.z = zShift;
        // moveDirection = moveDirection.normalized;
        
        // transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * toTargetRotation);

        /*testCamera.rotation.y*/ 
    }

    private void Move()
    {
        rb.MovePosition(transform.position + transform.forward * input.normalized.magnitude * runSpeed * Time.deltaTime);
    }
}

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix; 
    
    public static Vector3 ToIso(this Vector3 input, float cameraAngle)
    {
        _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, cameraAngle, 0));
        
        return _isoMatrix.MultiplyPoint3x4(input);
    } 
}
