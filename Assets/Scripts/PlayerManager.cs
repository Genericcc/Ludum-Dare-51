using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private Movement movement;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        //inputManager.AllInputs();
    }

    private void FixedUpdate()
    {
        //movement.AllMovement();
    }

}
