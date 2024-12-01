/*
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class OldPlayerController : Singleton<PlayerController>
{
    [SerializeField]private UnityEvent<CharacterStats> onHealthChange;

    [SerializeField]private PhysicsMaterial2D wallMat;
    [SerializeField]private PhysicsMaterial2D normalMat;

    private PlayerAnimation playerAnimation;
    public PlayerInputControl inputControl;
    public Vector2 inputDir;

    public OutSideSkill outSideSkill;
    public InSideSkill inSideSkill;

    private PlayerStats playerStats;
    private PhysicsCheck physicsCheck;
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D collider;

    public bool jumpInputBuffer;
    public int jumpCount;  // 跳跃计数

    override protected void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        collider = GetComponent<CapsuleCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
        inputControl.Enable();
        inputControl.GamePlay.Attack.started += PlayerAttack;
        inputControl.GamePlay.ChangeStats.performed += ChangePlayerStats;
        inputControl.GamePlay.Jump.canceled += delegate{
            jumpInputBuffer = false;
        };
    }

    private void Start()
    {
        onHealthChange?.Invoke(playerStats);
        playerStats.skillStats = PlayerSkillStats.OutSideWorld;
        outSideSkill.enabled = true;
    }

    private void Update()
    {
        inputDir = inputControl.GamePlay.Move.ReadValue<Vector2>();
        CheckState();
        ChangePlayerBar();

        // 预输入机制
        if (jumpInputBuffer && physicsCheck.isGround)
        {
            float speed = playerStats.JumpHeight / playerStats.JumpTime;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
            jumpCount = 1;
            jumpInputBuffer = false;
        }

        if (rigidBody.velocity.y == 0 && physicsCheck.isGround)
        {
            jumpCount = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!playerStats.isHurt && !playerStats.isAttack) {
            MovePlayer();
        }
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void ChangePlayerStats(InputAction.CallbackContext context)
    {
        if (!(context.interaction is HoldInteraction)) return;
        switch(playerStats.skillStats){
            case PlayerSkillStats.InSideWold:{
                playerStats.skillStats = PlayerSkillStats.OutSideWorld;
                outSideSkill.enabled = true;
                inSideSkill.enabled = false;
                break;
            }
            case PlayerSkillStats.OutSideWorld:{
                playerStats.skillStats = PlayerSkillStats.InSideWold;
                inSideSkill.enabled = true;
                outSideSkill.enabled = false;
                break;
            }
        }
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        playerStats.isAttack = true;
    }

    private void MovePlayer()
    {
        var localScale = transform.localScale;
        int filp = localScale.x * inputDir.x >= 0 ? 1 : -1;
        rigidBody.velocity = new Vector2(inputDir.x * playerStats.MaxRunSpeed, rigidBody.velocity.y);
        transform.localScale = new Vector3(filp * localScale.x, localScale.y, localScale.z);
    }

    private void CheckState()
    {
        collider.sharedMaterial = physicsCheck.isGround ? normalMat : wallMat;
    }

    private void ChangePlayerBar()
    {
        // 传递血量改变事件,更新玩家血条
        if (playerStats.isHurt)
            onHealthChange?.Invoke(playerStats);
    }


    #region "Unity Event"
    public void PlayerHurt(Transform attacker)
    {
        if (playerStats.isHurt) return;
        playerStats.isHurt = true;
        rigidBody.velocity = Vector2.zero;
        float hurtForce = attacker.GetComponent<CharacterStats>().KnockFactor;
        Vector2 dir = new Vector2((transform.position - attacker.position).x, 0).normalized;
        rigidBody.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        playerStats.isDead = true;
        inputControl.GamePlay.Disable();
    }
    #endregion
}
*/