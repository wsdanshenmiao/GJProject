using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/TransformCube", fileName = "TransformCube")]
public class TransformCube : PlayerState
{
    public PlayerCube cubePrefab;
    float preGravity = 0;

    public override void Enter()
    {
        base.Enter();
        playerController.canChangeScale = false;
        // 进入方块状态后无重力
        playerController.SetVelocity(Vector3.zero);
        preGravity = playerController.GetGravity();
        playerController.SetGravity(0);

        if(GameManager.Instance.worldStates == WorldStates.INSIDE){
            long distance = -GameManager.Instance.twoWorldDistance;
            Vector3 playerPos = playerController.transform.position;
            Vector3 cubePos = new Vector3(playerPos.x + distance, playerPos.y, playerPos.z);
            DropsGeneration.Instance.SpawnDrops(cubePrefab.DropsData.DropsID, cubePrefab.DropCount, cubePos);
        }
    }

    public override void LogicUpdate()
    {
        // 动画播放完成前不能变状态
        var info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime < 0.95 && info.IsName("TransformCube")) return;
        animator.CrossFade(Animator.StringToHash("PlayerCube"), 0);

        if (!playerInput.transCubeState) return;
        if(playerData.isHurt){
            stateMachine.SwitchState(typeof(Hurt));
        }
        if (IsClimp()){
            stateMachine.SwitchState(typeof(Climp));
        }
        if(playerController.isGrounded){
            stateMachine.SwitchState(typeof(Wait));
        }
        if (!playerController.isGrounded)
        {
            stateMachine.SwitchState(typeof(Fall));
        }
    }

    public override void Exit()
    {
        // 恢复重力
        playerController.SetGravity(preGravity);
        playerController.canChangeScale = true;
    }
}
