using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{

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

    }

    public void Defend(bool isDefending)
    {
        playerHealth.SetIsDefending(isDefending);
    }

    private void InputManager_OnStartedAttacking(object sender, EventArgs e)
    {
        Attack();
    }

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPointTransform.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            //Debug.Log("Hit enemy is: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(100);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(attackPointTransform.position, attackRange);    
    }

}
