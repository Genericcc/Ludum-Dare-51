using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*questions: should I make separate classes for the Animator?*/
public class Movement : MonoBehaviour
{
    public static event EventHandler OnMovementStarted;
    public static event EventHandler OnMovementStopped;

    [HideInInspector]
    public AnimatorManager animatorHandler;

    [SerializeField] private LayerMask groudLayerMask;
    [SerializeField] private float downpull;
    [SerializeField] private float runSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool isActive;

    private InputManager inputManager;
    private Vector3 moveDirection;
    private Quaternion newRotationQuaternion;

    private CapsuleCollider coll;
    private Rigidbody rb;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
   
        animatorHandler = GetComponentInChildren<AnimatorManager>();
    }

    private void Start() 
    {
        InputManager.OnStartedAttacking += InputManager_OnStartedAttacking;
        InputManager.OnStartedDefending += InputManager_OnStartedDefending; 
        InputManager.OnFinishedDefending += InputManager_OnFinishedDefending; 
    }

    //Events region
    #region 

    private void InputManager_OnStartedAttacking(object sender, EventArgs e)
    {
        isActive = true;
    }

    //called by an event on animation
    private void OnFinishedAttacking()
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
       // Gravity();
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
        //SetVector();

        HandleMovement();
        //HandleRotation();
    }

    private void SetVector()
    {
        
        //moveDirection = moveDirection.normalized;
        
    }

    private void HandleMovement()
    {
        float xShift = InputManager.Instance.GetMoveVector().x * runSpeed * Time.deltaTime;
        float yShift = InputManager.Instance.GetMoveVector().z * runSpeed * Time.deltaTime;

        moveDirection = new Vector3(xShift, 0, yShift);
        moveDirection = moveDirection.normalized;

        float toTargetRotation = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * toTargetRotation);

        if(!isActive)
        {

            if(moveDirection != Vector3.zero)
            {
                OnMovementStarted?.Invoke(this, EventArgs.Empty);
                animatorHandler.StartWalking(true);

                transform.position += moveDirection * Time.deltaTime * runSpeed;
            }
            else
            {
                OnMovementStopped?.Invoke(this, EventArgs.Empty);
                animatorHandler.StartWalking(false);
            }
        }
    }

    private void HandleRotation()
    {
        if(moveDirection != Vector3.zero)
        {
            Quaternion tmpRotation = Quaternion.LookRotation(moveDirection);
            newRotationQuaternion = Quaternion.Slerp(this.transform.rotation, tmpRotation, rotationSpeed * Time.deltaTime);

            
            transform.rotation = newRotationQuaternion;
        }

    }


}
