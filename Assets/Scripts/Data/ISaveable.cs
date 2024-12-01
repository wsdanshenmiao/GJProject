
using System.Diagnostics;

/// <summary>
/// 可保存物体的统一接口
/// </summary>
public interface ISaveable
{
    DataDefiantion GetDataID();
    void RegisterSaveData() => DataManager.Instance?.RegisterSaveData(this);
    void UnRegitsterSaveData() => DataManager.Instance?.UnRegitsterSaveData(this);
    void GetSaveData(Data data);
    void LoadData(Data data);
}