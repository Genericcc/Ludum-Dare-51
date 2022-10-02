using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPointTransform;
    [SerializeField] float attackRange;
    //[SerializeField] float moveSpeed = 20f;


    #region AlgessarCode //This is my new additions to the code. I changed the name!

    PlayerControls playerControls;
    AnimatorManager animManager;

    public Vector2 movementInput;
    public float moveAmount;
    public Vector2 cameraInput;
    public float verticalInput;
    public float horizontalInput;
    public float mouseX;
    public float mouseY;



    private void Awake()
    {
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
        //HandleJump();
        //HandleAction();

    }



    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + MathF.Abs(verticalInput));
        animManager.UpdateAnimatorValues(moveAmount, 0);
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }


    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        //MovePlayer();
        
        if(Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    /*private void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue, 0, zValue);     
    }*/

    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPointTransform.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit enemy is: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(100);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(attackPointTransform.position, attackRange);    
    }
}
