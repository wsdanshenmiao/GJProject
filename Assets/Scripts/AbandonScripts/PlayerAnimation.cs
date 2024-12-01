/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
        private Animator animator;
        private Rigidbody2D rigidBody;
        private PhysicsCheck physicsCheck;
        private CharacterStats character;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
            physicsCheck = GetComponent<PhysicsCheck>();
            character = GetComponent<CharacterStats>();
        }

        // Update is called once per frame
        void Update()
        {
            SetAnimation();
        }

        private void SetAnimation()
        {
            animator.SetBool("Run", Mathf.Abs(rigidBody.velocity.x) > 0);
            animator.SetBool("IsGround", physicsCheck.isGround);
            animator.SetBool("IsDead", character.isDead);
        }

        public void PlayerAttack()
        {
            animator.SetTrigger("Attack");
        }
}
*/