using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public static event EventHandler OnStartedAttacking;
    public static event EventHandler OnFinishedAttacking;

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPointTransform;
    [SerializeField] float attackRange;

    private enum State{
        Swinging,
        AfterSwinging,
        Idle
    }
    
    private State state;
    private PlayerHealth playerHealth;
    float stateTimer;

    private void Start() 
    {
        playerHealth = GetComponent<PlayerHealth>();

        InputManager.OnStartedAttacking += InputManager_OnStartedAttacking ;

        state = State.Idle;
        stateTimer = 0;
    }

    private void Update() 
    {
        if(InputManager.Instance.IsMouseRightPressed()) 
        { 
            Defend(true); 
        }
        else
        {
            Defend(false);
        }

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
        //Starts the animation
        OnStartedAttacking?.Invoke(this, EventArgs.Empty);
        state = State.Swinging;
        
        //OverlapSphere Attack will execute in the middle of aniation
        stateTimer = .75f;
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

        OnFinishedAttacking?.Invoke(this, EventArgs.Empty);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(attackPointTransform.position, attackRange);    
    }

}
