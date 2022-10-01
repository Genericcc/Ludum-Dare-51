using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPointTransform;
    [SerializeField] float attackRange;
    [SerializeField] float moveSpeed = 20f; 

    void Start()
    {
        
    }

    void Update()
    {
        MovePlayer();
        
        if(Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    private void MovePlayer()
    {
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue, 0, zValue);     
    }

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
