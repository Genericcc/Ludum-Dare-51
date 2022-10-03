using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
       
    public Animator anim;
    int vertical;
    int horizontal;
    public bool canRotate;

    
    public void Awake()
    {
        anim = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    private void Start() 
    {
        InputManager.OnStartedAttacking += InputManager_OnStartedAttacking; 
        InputManager.OnStartedDefending += InputManager_OnStartedDefending; 
        InputManager.OnFinishedDefending += InputManager_OnFinishedDefending; 
    }

    private void InputManager_OnStartedAttacking(object sender, EventArgs e)
    {
        anim.SetTrigger("IsAttacking");
    }

    private void InputManager_OnStartedDefending(object sender, EventArgs e)
    {
        anim.SetBool("IsDefending", true);
    }

    private void InputManager_OnFinishedDefending(object sender, EventArgs e)
    {
        anim.SetBool("IsDefending", false);
    }

    #region Soulslike animator
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        //Animation snapping
        float snappedHorizontal;
        float snappedVertical;

        #region snappedVertical
        
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if(verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }

        #endregion 

        #region snappedHorizontal
        

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion


        anim.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);

    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }
    #endregion

    public void StartWalking(bool isWalking)
    {
        anim.SetBool("IsWalking", isWalking);
    }

}
