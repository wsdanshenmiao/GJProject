using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据类
/// </summary>
public class Data
{
    public string sceneToSave;
    public Dictionary<string, Vector3> characPos = new Dictionary<string, Vector3>();
    public Dictionary<string, float> characMaxHP = new Dictionary<string, float>();

    public void SaveScene(GameSceneSO gameScene)
    {
        sceneToSave = JsonUtility.ToJson(gameScene);
    }

    public GameSceneSO GetScene()
    {
        GameSceneSO ret = ScriptableObject.CreateInstance<GameSceneSO>();
        JsonUtility.FromJsonOverwrite(sceneToSave, ret);
        return ret;
    }
}