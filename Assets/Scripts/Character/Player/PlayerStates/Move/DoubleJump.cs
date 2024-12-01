using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/DoubleJump", fileName = "DoubleJump")]
public class DoubleJump : PlayerState
{
    //float originalGravity;
    public override void Enter()
    {
        CheckPillow();
        stateStartTime = Time.time;
        playerController.canAirJump = false;
        //originalGravity = -Physics2D.gravity.y;  // 物理系统中的重力

        float doubleJumpSpeed = Mathf.Sqrt(-2 * playerController.currentGravity * playerData.doubleJumpHeight);
        playerController.SetVelocityY(doubleJumpSpeed);
        Debug.Log("二段跳速度:" + doubleJumpSpeed);
        stateMachine.PlayAudioClip("Jump");
    }

    public override void Exit()
    {
        //Debug.Log("执行二段跳跳跃退出");
        base.Exit();
    }

    public override void LogicUpdate()
    {
        //Debug.Log("二段跳");
        if(CanTransCube()){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if (playerController.isFalling)
        {
            //Debug.Log("切换为掉落状态");
            stateMachine.SwitchState(typeof(Fall));
        }

        // 切换为冲刺状态
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }

        if(playerInput.isJump){
            playerController.SetJumpInputBufferTimer();
        }
    }

    public override void PhysicUpdate()
    {
        playerController.Move(playerData.walkSkySpeed);
    }

}
