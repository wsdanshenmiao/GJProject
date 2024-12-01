using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> loadRequestEvent;

    public void RaiseLoadRequestEvent(GameSceneSO gameScene, Vector3 pos, bool fadeScreen)
    {
        loadRequestEvent?.Invoke(gameScene, pos, fadeScreen);
    }
}
