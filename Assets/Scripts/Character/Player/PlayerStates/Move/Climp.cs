using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Climp", fileName = "Climp")]
public class Climp : PlayerState
{
    float preGravity = 0;

    public override void Enter()
    {
        CheckPillow();
        // 进入攀爬状态后无重力
        playerController.SetVelocity(Vector3.zero);
        preGravity = playerController.GetGravity();
        playerController.SetGravity(0);
        stateMachine.PlayAudioClip("Climp");
    }

    public override void LogicUpdate()
    {
        //Debug.Log("Climp");
        if(CanTransCube()){
            stateMachine.SwitchState(typeof(TransformCube));
        }
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
        }
        if (!playerInput.canClimp)
        {
            stateMachine.SwitchState(typeof(Walk));
        }
        currentSpeed = playerData.climpHorizontalSpeed;
    }

    public override void PhysicUpdate()
    {
        playerController.SetVelocityY(playerInput.climpDir * playerData.climpSpeed * Time.deltaTime);
        playerController.Move(currentSpeed);
    }

    public override void Exit()
    {
        playerController.SetGravity(preGravity);
    }

}
