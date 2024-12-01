using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/JumpUpWalk", fileName = "JumpUpWalk")]
public class JumpUpWalk : PlayerState
{
    //float originalGravity;
    //float gravityRatio;
    float jumpSpeed;
    //float currentGravity;
    public override void Enter()
    {
        playerController.hasJumpInputBuffer = false;   // 将跳跃预输入值设为false
        CheckPillow();
        stateStartTime = Time.time;
        /*
        originalGravity = Physics2D.gravity.y;  // 物理系统中的重力
        currentGravity = -1f * (2*playerData.jumpHeight) / (playerData.jumpTime * playerData.jumpTime); // 计算出需要的重力加速度
        gravityRatio = currentGravity / originalGravity; // 计算出重力系数
        playerController.rigidBody.gravityScale = gravityRatio; // 设置重力系数
        */
        jumpSpeed = -1f * playerController.currentGravity * playerData.jumpTime; // 求出起跳速度
        playerController.SetVelocityY(jumpSpeed);

        stateMachine.PlayAudioClip("Jump");
    }

    public override void LogicUpdate()
    {
        Debug.Log("进入一段跳");
        if(CanTransCube()){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if (playerController.isFalling)
        {
            //Debug.Log("切换为掉落状态");
            stateMachine.SwitchState(typeof(Fall));
        }
        // 跳跃时直接二段跳
        if(playerInput.isJump)
        {
            if(playerController.canAirJump)
            {
                stateMachine.SwitchState(typeof(DoubleJump));
            }
        }
        // 切换为冲刺状态
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }
        if(playerController.isGrounded){
            stateMachine.SwitchState(typeof(Land));
        }
    }

    public override void PhysicUpdate()
    {
        playerController.Move(playerData.walkSkySpeed);
    }

    public override void Exit()
    {
        //Debug.Log("执行跳跃退出");
        base.Exit();
        
    }

}