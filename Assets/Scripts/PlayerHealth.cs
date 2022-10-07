using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 100;
    private bool isDefending;

    private void Start() 
    {
        isDefending = false;
    }

    private void Update() 
    {
        
    }

    public void TakeDamage(int damage) 
    {
        if(!isDefending)
        {
            hitPoints -= damage;
            Debug.Log("Target health is: " + hitPoints);
        }
        
        if(hitPoints <= 0f) 
        {
            Destroy(gameObject);
        }
    }

    public void SetIsDefending(bool isDefending)
    {
        this.isDefending = isDefending;
    }

}
