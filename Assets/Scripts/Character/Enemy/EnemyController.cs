using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private VoidEventSO findPlayerEvent;
    [SerializeField] private VoidEventSO lostPlayerEvent;
    private Enemy enemyStats;

    // private Vector3 initialPos;
    // private Vector3 nextPos;

    GameObject chaseTarget;
    private Seeker seeker;

    private bool hadFindPlayer = false;

    private List<Vector3> pathPointsList; // 路径点列表
    private int currentIndex = 0;     // 当前路径点的索引
    private float pathGenerateInterval = 0.5f;  // 路径生成间隔
    private float pathGenerateTimer = 0;        // 路径生成计时器

    private void Awake()
    {
        enemyStats = GetComponent<Enemy>();
        seeker = GetComponent<Seeker>();
    }

    private void Start()
    {
        // initialPos = transform.position;
        // nextPos = GetRandomPos();
    }

    private void FixedUpdate()
    {
        bool findPlayer = FindPlayer();
        if(findPlayer){
            if(!hadFindPlayer){
                hadFindPlayer = true;
                findPlayerEvent.RaiseEvent();
            }
        }
        else{
            if(hadFindPlayer){
                hadFindPlayer = false;
                lostPlayerEvent.RaiseEvent();
            }
        }
    }
    
    private void Update()
    {
        if(chaseTarget == null) return;
        AutoPath();
        
        float distance = Vector2.Distance(chaseTarget.transform.position, transform.position);
        if(distance < enemyStats.GuardingRange)
        {
            if(pathPointsList == null)
            {
                return;
            }
            if(distance <= enemyStats.StopDistance)
            {
                // 攻击玩家
            }
            else
            {
                // 追逐玩家
                MoveToTarget(pathPointsList[currentIndex]);
            }
        }
        
    }

    private bool FindPlayer()
    {
        var collider = Physics2D.OverlapCircleAll(transform.position, enemyStats.GuardingRange);
        foreach (var hitCollider in collider) {
            if (hitCollider.gameObject.tag == "Player") {
                chaseTarget = hitCollider.gameObject;
                return true;
            }
        }
        return false;
    }

    // private void ChaseMode()
    // {
    //     if(chaseTarget){
    //         MoveToTarget(chaseTarget.transform.position);
    //     } 
    // }

    // private void PatrolMode()
    // {
    //     // 正在移动，不寻找下一个巡逻点
    //     if(Vector3.Distance(transform.position,nextPos) > enemyStats.StopDistance){
    //         MoveToTarget(nextPos);
    //     }
    //     else{
    //         // 停留时间结束
    //         if(enemyStats.currLookUpTime <0){
    //             nextPos = GetRandomPos();
    //         }
    //         else{
    //             enemyStats.currLookUpTime -= Time.deltaTime;
    //         }
    //     }
    // }

    private void MoveToTarget(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;
        Vector3 pos = transform.position + dir * enemyStats.maxWalkSpeed * Time.deltaTime;
        //rigidBody.MovePosition(pos);
        transform.position = pos;
    }

    // private Vector3 GetRandomPos()
    // {
    //     float patrolRadium = enemyStats.GuardingRange;
    //     float randomX = UnityEngine.Random.Range(-patrolRadium, patrolRadium);
    //     Vector3 randomPoint =new Vector3(initialPos.x + randomX, transform.position.y, transform.position.z);
    //     return randomPoint;
    // }

    private void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        seeker.StartPath(transform.position, target, Path =>
        {
            pathPointsList = Path.vectorPath;
        });
    }

    private void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if(chaseTarget != null)
        {
            if (pathGenerateTimer >= pathGenerateInterval)
            {
                GeneratePath(chaseTarget.transform.position);
                pathGenerateTimer = 0;
            }
            if((pathPointsList == null || pathPointsList.Count <= 0))
            {
                GeneratePath(chaseTarget.transform.position);
            }
            else if (Vector2.Distance(transform.position, pathPointsList[currentIndex]) < 0.1f)
            {
                currentIndex++;
                if (currentIndex >= pathPointsList.Count)
                {
                    GeneratePath(chaseTarget.transform.position);
                }
            }
        }
        
    }
}
