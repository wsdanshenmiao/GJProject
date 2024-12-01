/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InSideSkill : MonoBehaviour
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
        PlayerController.Instance.inputControl.GamePlay.Jump.started += PlayerJump;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        ChangeSize();
        TransformCube();
    }

    private void OnDisable()
    {
        PlayerController.Instance.inputControl.GamePlay.Jump.started -= PlayerJump;
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        float speed = playerStats.JumpHeight / playerStats.JumpTime;
        if (physicsCheck.isGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
            PlayerController.Instance.jumpCount = 1;
        }
    }

    private void TransformCube()
    {
    }

    private void ChangeSize()
    {
    }

    private void Attack()
    {
       
    }
}
*/