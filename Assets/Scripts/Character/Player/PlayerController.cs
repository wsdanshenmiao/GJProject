using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerSizeState{
    NORMAL = 0,SMALL = 1,BIG = 2
}

public class PlayerController : MonoBehaviour
{
    [Header("事件监听")]
    [SerializeField] SceneLoadEventSO sceneLoadEvent;
    [SerializeField] SceneLoadEventSO sceneUnloadedEvent;
    [SerializeField] VoidEventSO afterSceneLoadEvent;
    //public PlayerDataSO playerData;
    PlayerInput playerInput;
    PlayerGroundDetector groundDetector;
    public Player playerStats;

    Vector3 nextTransportPos;
    public Vector3 prePlayerPos;

    public Rigidbody2D rigidBody;
    public PlayerSizeState sizeState;

    WaitForSeconds waitJumpBufferTime;            // 配合预输入等待时间的协程

    public bool canAirJump { get; set; }
    public bool victory { get; private set; }
    public bool isGrounded => groundDetector.isGrounded;                // 是否在地面上
    public bool isFalling => (rigidBody.velocity.y < 0f) && (!isGrounded);  // 是否在下落
    public bool CanSprint => SprintCDTime >= playerStats.sprintCD && playerInput.isSprint;  // 能否冲刺
    public float SprintCDTime;                                      // 记录已经等待的时间

    public bool canChangeScale = true;
    public bool hasJumpInputBuffer{ get; set; }   // 是否有跳跃预输入
    public bool enablePillow = false;

    public float moveSpeed => Mathf.Abs(rigidBody.velocity.x);
    
    // 重力相关
    float originalGravity;
    public float gravityRatio;
    float jumpSpeed;
    public float currentGravity;

    private void Awake()
    {
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        playerInput = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();

        playerStats = GetComponent<Player>();

        SprintCDTime = playerStats.sprintCD;  // 开始时应该可以立即冲刺
    }

    private void OnEnable()
    {
        playerInput.EnableGameplayInputs();
        sceneLoadEvent.loadRequestEvent += OnSceneLoad;
        sceneUnloadedEvent.loadRequestEvent += OnSceneUnloaded;
        afterSceneLoadEvent.onEventRaised += AfterSceneLoad;
    }

    void Start()
    {
        //Debug.Log("冲刺CD:" + playerStats.sprintCD);
        waitJumpBufferTime = new WaitForSeconds(playerStats.hasJumpInputBufferTime);


        // 重力系数设定
        originalGravity = Physics2D.gravity.y;  // 物理系统中的重力
        //Debug.Log("原始重力:" + originalGravity);
        //Debug.Log("跳跃高度:" + playerStats.jumpHeight + " 跳跃时间:" + playerStats.jumpTime);
        currentGravity = -1f * (2*playerStats.jumpHeight) / (playerStats.jumpTime * playerStats.jumpTime); // 计算出需要的重力加速度
        //Debug.Log("现在应有的重力:" + currentGravity);
        gravityRatio = currentGravity / originalGravity; // 计算出重力系数
        
        rigidBody.gravityScale = gravityRatio; // 设置重力系数
        //Debug.Log("原初重力系数为:" + gravityRatio);

        float normalSize = playerStats.normalSize;
        sizeState = PlayerSizeState.NORMAL;
        transform.localScale = new Vector3(normalSize, normalSize, normalSize);
    }

    private void Update()
    {
        if(canChangeScale){
            ChangePlayerSize();
        }
        SprintCDTime += Time.deltaTime;
        //Debug.Log("现在的重力系数为" + rigidBody.gravityScale);
    }

    private void OnDisable()
    {
        playerInput.DisableGameplayInputs();
        sceneLoadEvent.loadRequestEvent += OnSceneLoad;
        sceneUnloadedEvent.loadRequestEvent += OnSceneUnloaded;
        afterSceneLoadEvent.onEventRaised += AfterSceneLoad;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Climbable")){
            playerInput.canClimp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("Climbable")){
            playerInput.canClimp = false;
        }
    }
    
    /// <summary>
    /// 完成场景加载后
    /// </summary>
    private void AfterSceneLoad()
    {
        transform.position = nextTransportPos;
        gameObject.SetActive(true);
        playerInput.EnableGameplayInputs();
    }

    private void ChangePlayerSize()
    {
        if(playerInput.isChangeSize){
            sizeState = (PlayerSizeState)(((int)sizeState + 1) % 3);
            float x = transform.localScale.x / transform.localScale.x;
            Vector3 scale = new Vector3(x, 1, 1);
            switch(sizeState){
                case PlayerSizeState.NORMAL:{
                        playerStats.defence = 0;
                        transform.localScale = scale * playerStats.normalSize; break;
                }
                case PlayerSizeState.SMALL:{
                        playerStats.defence = 0;
                        transform.localScale = scale * playerStats.smallSize; break;
                }
                case PlayerSizeState.BIG:{
                        playerStats.defence = 1;
                        transform.localScale = scale * playerStats.bigSize; break;
                }
            }
        }
        if(playerInput.isMove){
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(playerInput.moveX * Mathf.Abs(scale.x), scale.y, scale.z);
        }
    }

    /// <summary>
    ///  加载场景时
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnSceneLoad(GameSceneSO arg0, Vector3 pos, bool arg2)
    {
        playerInput.DisableGameplayInputs();
    }

    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    /// <summary>
    /// 卸载当前场景时
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="pos"></param>
    /// <param name="arg2"></param>
    private void OnSceneUnloaded(GameSceneSO arg0, Vector3 pos, bool arg2)
    {
        nextTransportPos = pos;
        gameObject.SetActive(false);
    }

    IEnumerator JumpInputBufferCoroutine()
    {
        hasJumpInputBuffer = true;

        yield return waitJumpBufferTime;

        hasJumpInputBuffer = false;
    }

    public void Move(float speed)
    {
        SetVelocityX(speed * playerInput.moveX);
    }

    public void SetVelocity(Vector2 velocity)
    {
        rigidBody.velocity = velocity;
    }

    public void SetVelocityX(float velocityX)
    {
        rigidBody.velocity = new Vector2(velocityX, rigidBody.velocity.y);
    }

    public void SetVelocityY(float velocityY)
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, velocityY);
    }

    public void SetGravity(float gravity)
    {
        rigidBody.gravityScale = gravity;
    }

    public Vector2 GetVelocity()
    {
        return rigidBody.velocity;
    }

    public float GetGravity()
    {
        return rigidBody.gravityScale;
    }

}
