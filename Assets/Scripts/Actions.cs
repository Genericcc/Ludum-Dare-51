using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    public static Actions Instance { get; private set; }

    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform attackPointTransform;
    [SerializeField] float attackRange;

    private void Awake() 
    {
        if(Instance != null) 
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPointTransform.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit enemy is: " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(25);
        }
    }

    public void Defend()
    {
        Debug.Log("Defending.");
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(attackPointTransform.position, attackRange);    
    }
}
