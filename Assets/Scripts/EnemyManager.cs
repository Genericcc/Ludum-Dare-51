using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform attackPointTransform;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask playerMask;

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
            ChasePlayer();
        }
        else if (distanceToPlayer <= navMeshAgent.stoppingDistance) 
        {
            AttackPlayer();
        }    
    }

    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackPointTransform.position, attackRange, playerMask);

        foreach(Collider player in hitPlayer)
        {
            Debug.Log("Hit player is: " + player.name);
            player.GetComponent<PlayerHealth>().TakeDamage(50);
        }
    }

}
