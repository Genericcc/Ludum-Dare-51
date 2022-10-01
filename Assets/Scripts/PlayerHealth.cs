using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 100;

    public void TakeDamage(int damage) 
    {
        hitPoints -= damage;
        
        Debug.Log("Target health is: " + hitPoints);

        if(hitPoints <= 0f) 
        {
            Destroy(gameObject);
        }
    }

}
