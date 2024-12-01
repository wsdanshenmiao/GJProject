using System.Collections;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] protected string stateName;
    [SerializeField, Range(0, 1)] protected float tranditionDuration = .1f;

    protected int stateHash;

    protected Character characterData;
    protected Player playerData;
    
    protected float stateStartTime;

    protected float currentSpeed;

    protected Animator animator;

    protected PlayerController playerController;
    protected PlayerInput playerInput;
    protected PlayerStateMachine stateMachine;

    protected float stateDuration => Time.time - stateStartTime;
    protected bool  finishFadeIn = false;
    protected bool finishFadeOut = false;

    void OnEnable() 
    {
        stateHash = Animator.StringToHash(stateName);
    }

    public void Initialize(Animator animator, PlayerController player, PlayerInput input, PlayerStateMachine stateMachine)
    {
        this.animator = animator;
        this.playerController = player;
        this.playerInput = input;
        this.stateMachine = stateMachine;
        this.characterData = player.GetComponent<Character>();
        this.playerData = player.GetComponent<Player>();
        //this.playerData = player.playerData;
    }

    public virtual void Enter() 
    {
        animator.CrossFade(stateHash, tranditionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicUpdate() { }

    protected bool IsClimp()
    {
        if (!playerInput.canClimp) return false;
        return playerController.GetVelocity().y < 0 ? playerInput.canClimp : playerInput.climpDir > 0;
    }

    protected bool CanTransCube(){
        return playerInput.transCubeState && GameManager.Instance.worldStates == WorldStates.INSIDE;
    }

    protected void CheckPillow()
    {
        if(playerController.enablePillow){
            animator.CrossFade(Animator.StringToHash(stateName + "WithPillow"), tranditionDuration);
        }
        else{
            animator.CrossFade(Animator.StringToHash(stateName + "WithoutPillow"), tranditionDuration);
        }
    }

    protected IEnumerator FadeIn(FadeEventSO fadeEvent, float fadeIn)
    {
        // 进行淡入淡出
        fadeEvent.FadeIn(fadeIn);
        yield return new WaitForSeconds(fadeIn);
        finishFadeIn = true;
    }

    protected IEnumerator FadeOut(FadeEventSO fadeEvent, float fadeOut)
    {
        // 进行淡入淡出
        fadeEvent.FadeOut(fadeOut);
        yield return new WaitForSeconds(fadeOut);
        finishFadeOut = true;
    }

}
