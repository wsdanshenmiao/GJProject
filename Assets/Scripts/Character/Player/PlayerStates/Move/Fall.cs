using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Fall", fileName = "Fall")]
public class Fall : PlayerState
{
    public override void Enter()
    {
        CheckPillow();
        stateStartTime = Time.time;
    }

    public override void LogicUpdate()
    {
        //Debug.Log("下落");
        // 切换为着陆状态
        if(CanTransCube()){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
        }
        if (playerController.isGrounded)
        {
            stateMachine.SwitchState(typeof(Land));
        }
        // 切换为二段跳状态
        if(playerInput.isJump)
        {
            if(playerController.canAirJump)
            {
                stateMachine.SwitchState(typeof(DoubleJump));
                return;
            }
            playerController.SetJumpInputBufferTimer();
        }
        // 切换为冲刺状态
        if(playerController.CanSprint)
        {
            stateMachine.SwitchState(typeof(Sprint));
        }
    }

    public override void PhysicUpdate()
    {
        playerController.Move(playerData.walkSkySpeed);
    }
}
