using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Sprint", fileName = "Sprint")]
public class Sprint : PlayerState
{
    float durationTime;          // 记录已经冲刺的时间
    bool canChangeState => durationTime >= playerData.sprintDurationTime;
    float sprintSpeed = 5f;
    
    public override void Enter()
    {
        playerController.SetVelocityY(0f);                                      // 将Y轴速度设为0，同时让Y轴不受重力影响
        durationTime = 0f;                                                      // 设置冲刺持续时间为0
        //Debug.Log("冲刺更改前的重力系数" + playerController.rigidBody.gravityScale + "应该更改的系数:"+ playerController.gravityRatio);
        playerController.rigidBody.gravityScale = 0f;                           // 将重力设为0
        //Debug.Log("冲刺重力系数更改后" + playerController.rigidBody.gravityScale);
        playerController.SprintCDTime = 0f;                                     // 重置CD时间为0
        //Debug.Log("冲刺更改的重力系数" + playerController.rigidBody.gravityScale);
        sprintSpeed = playerData.sprintDistance / playerData.sprintDurationTime;  // 计算出冲刺的速度
        //Debug.Log("冲刺距离:" + playerData.sprintDistance);
        stateMachine.PlayAudioClip("Sprint");
        
    }

    public override void LogicUpdate()
    {
        //Debug.Log("冲刺！冲刺！冲！");
        durationTime += Time.deltaTime;
        if(canChangeState)
        {
            playerController.rigidBody.gravityScale = playerController.gravityRatio; // 将重力系数恢复为原来的重力系数
            //Debug.Log("冲刺结束后的重力系数:" + playerController.rigidBody.gravityScale);
            if(CanTransCube()){
                stateMachine.SwitchState(typeof(TransformCube));
            }
            if (IsClimp()){
                stateMachine.SwitchState(typeof(Climp));
            }
            if(!playerInput.isMove) {
                stateMachine.SwitchState(typeof(Wait));
            }
            if(playerInput.isStopRun) {
                stateMachine.SwitchState(typeof(Walk));
            }
            if (playerInput.isJump)
            {
                stateMachine.SwitchState(typeof(JumpUpRun));
            }
            if (playerController.isFalling)
            {
                //Debug.Log("切换为掉落状态");
                stateMachine.SwitchState(typeof(Fall));
            }

            if(playerController.isGrounded)
            {
                stateMachine.SwitchState(typeof(Wait));
            }

            if(playerInput.isJump && !playerController.isGrounded)
            {
                if(playerController.canAirJump)
                {
                    stateMachine.SwitchState(typeof(DoubleJump));
                }
            }

        }
        
    }

    public override void PhysicUpdate()
    {
        if(!canChangeState)
        {
            playerController.Move(sprintSpeed);                 // 保持冲刺速度
        }
        else{
            playerController.Move(0f);
        }
    }

}
