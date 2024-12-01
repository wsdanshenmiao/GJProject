using UnityEngine;

public class EnemyState : ScriptableObject, IState
{
    [SerializeField] protected string stateName;
    [SerializeField, Range(0, 1)] protected float tranditionDuration = 1f;

    protected int stateHash;
    protected Enemy enemyData;
    
    float stateStartTime;

    protected float currentSpeed;

    protected Animator animator;

    protected EnemyController playerController;
    protected EnemyStateMachine stateMachine;

    protected float stateDuration => Time.time - stateStartTime;

    void OnEnable() 
    {
        stateHash = Animator.StringToHash(stateName);
    }

    public void Initialize(Animator animator, EnemyController player,EnemyStateMachine stateMachine)
    {
        this.animator = animator;
        this.playerController = player;
        this.stateMachine = stateMachine;
        this.enemyData = player.GetComponent<Enemy>();
        
    }

    public virtual void Enter() 
    {
        animator.CrossFade(stateHash, tranditionDuration);
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicUpdate() { }

    
}
