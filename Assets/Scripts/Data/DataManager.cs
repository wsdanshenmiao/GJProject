using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 统一管理数据
/// </summary>
public class DataManager : Singleton<DataManager>
{
    [Header("事件监听")]
    [SerializeField] private VoidEventSO saveDataEvent;

    private Data saveData;

    private List<ISaveable> saveableList = new List<ISaveable>();

    override protected void Awake()
    {
        base.Awake();
        saveData = new Data();
    }

    private void OnEnable()
    {
        saveDataEvent.onEventRaised += SaveData;
    }

    private void OnDisable()
    {
        saveDataEvent.onEventRaised -= SaveData;
    }

    public void SaveData()
    {
        foreach(var saveable in saveableList){
            saveable.GetSaveData(saveData);
        }
    }

    public void LoadData()
    {
        foreach(var saveable in saveableList){
            saveable.LoadData(saveData);
        }
    }

    public void FixMaxHP(string id, float currHP){
        saveData.characMaxHP[id] = currHP;
    }

    /// <summary>
    /// 将要保存的数据注册
    /// </summary>
    /// <param name="saveable"></param>
    public void RegisterSaveData(ISaveable saveable)
    {
        if(!saveableList.Contains(saveable)){
            saveableList.Add(saveable);
        }
    }

    /// <summary>
    /// 取消数据的注册
    /// </summary>
    /// <param name="saveable"></param>
    public void UnRegitsterSaveData(ISaveable saveable)
    {
        saveableList.Remove(saveable);
    }
}
