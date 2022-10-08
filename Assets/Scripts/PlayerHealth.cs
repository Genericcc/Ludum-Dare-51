using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event EventHandler OnPlayerDeath;

    [SerializeField] int hitPoints = 100;
    private bool isDefending;

    private void Start() 
    {
        isDefending = false;
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
            hitPoints = 0;
            ProcessDeath();
        }
    }

    public void SetIsDefending(bool isDefending)
    {
        this.isDefending = isDefending;
    }

    private void ProcessDeath()
    {
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);

        //Destroy(gameObject);
    }

}
