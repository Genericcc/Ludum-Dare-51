using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    private enum State{
        Swinging,
        Idle
    }
    public static event EventHandler OnStartedAttacking;
    public static event EventHandler OnFinishedAttacking;

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPointTransform;
    [SerializeField] float attackRange;
    float damageDelay = .4f;

    private PlayerHealth playerHealth;
    private State state;
    private bool isActive;
    private float stateTimer;


    private void Start() 
    {
        playerHealth = GetComponent<PlayerHealth>();

        InputManager.OnStartedAttacking += InputManager_OnStartedAttacking;

        InputManager.OnStartedDefending += InputManager_OnStartedDefending;
        InputManager.OnFinishedDefending += InputManager_OnFinishedDefending;

        state = State.Idle;
        stateTimer = 0;
    }

    private void InputManager_OnStartedDefending(object sender, EventArgs e)
    {
        if(!isActive)
        {
            isActive = true;
            Defend(true); 
        }  
    }

    private void InputManager_OnFinishedDefending(object sender, EventArgs e)
    {
        isActive = false;
        Defend(false);
    }

    private void Update() 
    {
        stateTimer -= Time.deltaTime;

        if(stateTimer <= 0 && state == State.Swinging)
        {
            Attack();
        }
    }

    public void Defend(bool isDefending)
    {
        playerHealth.SetIsDefending(isDefending);
    }

    private void InputManager_OnStartedAttacking(object sender, EventArgs e)
    {
        if(!isActive)
        {
            isActive = true;
            //Starts the animation
            OnStartedAttacking?.Invoke(this, EventArgs.Empty);
            state = State.Swinging;
            
            //OverlapSphere Attack will execute in the middle of aniation

            stateTimer = damageDelay;
        } 
    }

    private void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPointTransform.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            //Debug.Log("Hit enemy is: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(100);
        }

        state = State.Idle;

        isActive = false;
        OnFinishedAttacking?.Invoke(this, EventArgs.Empty);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(attackPointTransform.position, attackRange);    
    }

}
