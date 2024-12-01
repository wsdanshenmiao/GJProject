using UnityEngine;


public class Enemy : Character
{
    [SerializeField] private EnemyDataSO templateEnemyData;
    private EnemyDataSO enemyData;
    
    
    //private Transform player;


    public float currLookUpTime = 0;
    public bool isPatrol = true;

    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void OnNewGameEvent()
    {
        base.OnNewGameEvent();
        if(!enemyData){
            ResetStats();   
        }
        else{
            currLookUpTime = templateEnemyData.lookUpTime;
            isPatrol = true;
        }
    }

    override protected void Update()
    {
        base.Update();
    }

    new protected void ResetStats()
    {
        enemyData = Instantiate(templateEnemyData);
        currLookUpTime = templateEnemyData.lookUpTime;
        isPatrol = true;
    }

    // 获取路径点
    

    #region Read From CharacterDataSO
    public float MoveRange{
        get{ return enemyData ? enemyData.moveRange : 0; }
        set{ enemyData.moveRange = value; }
    }
    public float GuardingRange{
        get{ return enemyData ? enemyData.guardingRange : 0; }
        set{ enemyData.guardingRange = value; }
    }
    public float LookUpTime{
        get { return enemyData ? enemyData.lookUpTime : 0; }
        set{ enemyData.lookUpTime = value; }
    }
    public float StopDistance{
        get { return enemyData ? enemyData.stopDistance : 0; }
        set{ enemyData.stopDistance = value; }
    }
    #endregion
}
