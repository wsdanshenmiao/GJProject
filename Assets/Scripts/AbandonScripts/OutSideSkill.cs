/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OutSideSkill : MonoBehaviour
{
    private PhysicsCheck physicsCheck;
    private Rigidbody2D rigidBody;
    private PlayerStats playerStats;

    private void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        PlayerController.Instance.inputControl.GamePlay.Jump.started += DoubleJump;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
    }

    private void OnDisable()
    {
        PlayerController.Instance.inputControl.GamePlay.Jump.started -= DoubleJump;
    }

   private void Sprint()
    {
       
    }

    private void DoubleJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        var player = PlayerController.Instance;
        float speed = playerStats.JumpHeight / playerStats.JumpTime;
        if (physicsCheck.isGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
            player.jumpCount = 1;
        }
        else if (player.jumpCount < 2)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
            player.jumpCount++;
        }
        else
        {
            player.jumpInputBuffer = true;
        }

    }
}
*/