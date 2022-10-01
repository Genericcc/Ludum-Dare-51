using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryTenSeconds : MonoBehaviour
{
    
    private float tenSecTimer;

    private void Start() 
    {
        tenSecTimer = 10f;
    }

    void Update()
    {
        tenSecTimer -= Time.deltaTime;

        //Debug.Log(tenSecTimer);

        if(tenSecTimer < 0.1f)
        {
            RandomBullshitGo();

            tenSecTimer = 10f;
        }

    }

    private void RandomBullshitGo()
    {
        //TODO add random bullshit
    }
}
