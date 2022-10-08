using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform attackPointTransform;
    [SerializeField] LayerMask playerMask;
    [SerializeField] int damage;
    [SerializeField] float attackRange;

    NavMeshAgent navMeshAgent;
    float distanceToPlayer = Mathf.Infinity;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(target.position, transform.position);

        EngagePlayer();
    }

    private void EngagePlayer()
    {
        if(distanceToPlayer > navMeshAgent.stoppingDistance) 
        {
            Debug.Log("Chasing");
            ChasePlayer();
        }
        else if (distanceToPlayer <= navMeshAgent.stoppingDistance) 
        {
            Debug.Log("Attacking");
            AttackPlayer();
        }    
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        //Debug.Log("Got here.");
        Collider[] hitPlayer = Physics.OverlapSphere(attackPointTransform.position, attackRange, playerMask);

        foreach(Collider player in hitPlayer)
        {
            Debug.Log("Hit player is: " + player.name);
            PlayerHealth pH = player.GetComponent<PlayerHealth>();
            pH.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(attackPointTransform.position, attackRange);    
    }
}
