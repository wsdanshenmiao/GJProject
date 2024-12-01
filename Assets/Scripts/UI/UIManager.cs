using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]private PlayerStateBar playerStateBar;

    [Header("事件监听")]
    [SerializeField] private SceneLoadEventSO sceneUnloadedEvent;
    [SerializeField] private CharacterEventSO healthEvent;

    private void OnEnable()
    {
        healthEvent.onEventRaised += OnHealthEvent;
        sceneUnloadedEvent.loadRequestEvent += OnSceneUnloadedEvent;
    }

    private void OnDisable()
    {
        healthEvent.onEventRaised -= OnHealthEvent;
        sceneUnloadedEvent.loadRequestEvent -= OnSceneUnloadedEvent;
    }

    private void OnSceneUnloadedEvent(GameSceneSO scene, Vector3 pos, bool fade)
    {
        bool isMenu = scene.sceneType == SceneType.MENU;
        playerStateBar.gameObject.SetActive(!isMenu);
    }


    private void OnHealthEvent(Character character)
    {
        playerStateBar.OnHealthChange(character);
    }

}
