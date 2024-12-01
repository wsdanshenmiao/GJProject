using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, ISaveable
{
    [SerializeField] private CharacterDataSO templateCharacterData;
    [SerializeField] private CharacterDataSO characterData;

    [Header("事件广播")]
    public UnityEvent<Character> onHealthChangeEvent;
    public UnityEvent<Transform> onHurtEvent;
    public UnityEvent onDieEvent;

    [Header("事件监听")]
    [SerializeField] private VoidEventSO newGameEvent;

    public bool isDead = false;
    public bool isHurt = false;

    // 当前血量
    private float m_CurrentHP = 3;
    public float currInvalidTime = 0;

    virtual protected void Awake()
    {
        OnNewGameEvent();
    }

    virtual protected void OnEnable()
    {
        newGameEvent.onEventRaised += OnNewGameEvent;
        ISaveable saveable = this;
        saveable.RegisterSaveData();
    }

    virtual protected void Update()
    {
        currInvalidTime += Time.deltaTime;
    }

    virtual protected void OnDisable()
    {
        newGameEvent.onEventRaised -= OnNewGameEvent;
        ISaveable saveable = this;
        saveable.UnRegitsterSaveData();
    }

    /// <summary>
    /// 子类重写加载自身独有数据
    /// </summary>
    virtual protected void OnNewGameEvent()
    {
        if (!characterData) {
            ResetStats();
        }
        else {
            currentHP = maxHP;
            currInvalidTime = 0;
            isDead = false;
            isHurt = false;
        }
    }

    /// <summary>
    /// 子类无需重写
    /// </summary>
    protected void ResetStats()
    {
        characterData = Instantiate(templateCharacterData);
        currentHP = maxHP;
        currInvalidTime = 0;
        isDead = false;
        isHurt = false;
        onHealthChangeEvent?.Invoke(this);
    }


    public void TakeDamege(Attack attacker)
    {
        // 无敌帧
        if (attacker == null || currInvalidTime < invalidFrame) return;  
        currInvalidTime = 0;

        float damege = Mathf.Max(attacker.CurrentDamege(this), 0);
        if (damege == 0) return;
        currentHP = Mathf.Max(0, currentHP - damege);
        onHealthChangeEvent?.Invoke(this);
        if(currentHP <= 0){
            isHurt = true;
            isDead = true;
            onDieEvent?.Invoke();
        }
        else{
            isHurt = true;
            onHurtEvent?.Invoke(attacker.transform);
        }
    }

    public DataDefiantion GetDataID()
    {
        return GetComponent<DataDefiantion>();
    }

    public void GetSaveData(Data data)
    {
        string id = GetDataID().dataID;
        if(data.characPos.ContainsKey(id)){
            data.characPos[id] = transform.position;
            data.characMaxHP[id] = maxHP;
        }
        else{
            data.characPos.Add(id, transform.position);
            data.characMaxHP[id] = maxHP;
        }
    }

    public void LoadData(Data data)
    {
        string id = GetDataID().dataID;
        if(data.characPos.ContainsKey(id)){
            transform.position = data.characPos[id];
            maxHP = data.characMaxHP[id];
            currentHP = maxHP;
            currInvalidTime = 0;
            onHealthChangeEvent.Invoke(this);
        }
    }


    // 读取填表属性
    #region Read From CharacterDataSO
    // 最大血量
    public float maxHP {
        get { return characterData ? characterData.maxHP : 0; }
        set {   float num = value - characterData.maxHP; 
                characterData.maxHP = value; 
                m_CurrentHP = m_CurrentHP + num;
                onHealthChangeEvent?.Invoke(this); }
    }
    virtual public float currentHP{
        get{ return m_CurrentHP; }
        set{ m_CurrentHP = value; }
    }
    // 速度
    public float maxRunSpeed {
        get { return characterData ? characterData.maxRunSpeed : 0; }
        set { characterData.maxRunSpeed = value; }
    }
    public float maxWalkSpeed{
        get{return characterData ? characterData.maxWalkSpeed : 0;}
        set{ characterData.maxWalkSpeed = value; }
    }
    // 无敌帧
    public float invalidFrame  {
        get { return characterData ? characterData.invalidFrame : 0; }
        set { characterData.invalidFrame = value; }
    }
    public float defence{
        get { return characterData ? characterData.defence : 0; }
        set{ characterData.defence = value; }
    }
    // 抗击退系数
    public float resistKnockFactor {
        get { return characterData ? characterData.resistKnockFactor : 0;}
        set { characterData.resistKnockFactor = value; }
    }
    public float knockFactor {
        get { return characterData ? characterData.knockFactor : 0; }
        set { characterData.knockFactor = value; }
    }
    #endregion

}
