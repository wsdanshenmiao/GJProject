using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [Header("事件监听")]
    [SerializeField] SceneLoadEventSO loadEvent;
    [SerializeField] VoidEventSO newGameEvent;

    [Header("事件广播")]
    [SerializeField] SceneLoadEventSO unloadedEvent;
    [SerializeField] VoidEventSO afterSceneLoadEvent;
    [SerializeField] FadeEventSO fadeEvent;

    public Vector3 initialPos;

    [SerializeField] GameSceneSO firstLoadScene;
    [SerializeField] GameSceneSO menuScene;
    private GameSceneSO currLoadScene;
    private GameSceneSO sceneToLoad;
    private Vector3 posToGo;
    private bool fadeScreen;
    
    public float fadeInDuration = 1;
    public float fadeOutDruation = 1;
    private bool isLoading = false;

    protected override void Awake()
    {
        base.Awake();
        // if(firstLoadScene){
        //     currLoadScene = firstLoadScene;
        //     currLoadScene.sceneRef.LoadSceneAsync(LoadSceneMode.Additive);
        // }
    }

    protected void OnEnable()
    {
        loadEvent.loadRequestEvent += OnLoadRequestEvent;
        newGameEvent.onEventRaised += OnNewGameEvent;
    }

    protected void Start()
    {
        if(menuScene){
            loadEvent.RaiseLoadRequestEvent(menuScene, Vector3.zero, true);
        }
        else{
            loadEvent.RaiseLoadRequestEvent(firstLoadScene, initialPos, true);
        }
        //NewGame();
    }

    protected void OnDisable()
    {
        loadEvent.loadRequestEvent -= OnLoadRequestEvent;
        newGameEvent.onEventRaised += OnNewGameEvent;
    }

    private void OnNewGameEvent()
    {
        sceneToLoad = firstLoadScene;
        // OnLoadRequestEvent(sceneToLoad, initialPos, true);
        // 加载场景并发送事件
        loadEvent.RaiseLoadRequestEvent(sceneToLoad, initialPos, true);
    }

    /// <summary>
    /// 场景加载事件请求
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="pos"></param>
    /// <param name="fade"></param>
    private void OnLoadRequestEvent(GameSceneSO scene, Vector3 pos, bool fade)
    {
        if (isLoading) return;
        isLoading = true;
        sceneToLoad = scene;
        posToGo = pos;
        fadeScreen = fade;
        StartCoroutine(UnLoadPreScene());
    }

    /// <summary>
    /// 卸载旧场景并加载新场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator UnLoadPreScene()
    {
        // 进行淡入淡出
        if(fadeScreen){
            fadeEvent.FadeIn(fadeInDuration);
        }
        yield return new WaitForSeconds(fadeInDuration);
        unloadedEvent.RaiseLoadRequestEvent(sceneToLoad, posToGo, true);
        if (currLoadScene) {
            yield return currLoadScene.sceneRef.UnLoadScene();
        }
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneRef.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// 场景完全加载后
    /// </summary>
    /// <param name="handle"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currLoadScene = sceneToLoad;
        StartCoroutine(ScreenFadeOut());
    }

    private IEnumerator ScreenFadeOut()
    {
        if(fadeScreen){
            fadeEvent.FadeOut(fadeOutDruation);
        }
        if(currLoadScene.sceneType == SceneType.LOCATION){
            afterSceneLoadEvent.RaiseEvent();
        }
        yield return new WaitForSeconds(fadeOutDruation);
        DataManager.Instance.SaveData();
        isLoading = false;
    }

    public DataDefiantion GetDataID()
    {
        return GetComponent<DataDefiantion>();
    }

    public void GetSaveData(Data data)
    {
        data.SaveScene(currLoadScene);
    }

    public void LoadData(Data data)
    {
        string playerID = Player.Instance.GetComponent<DataDefiantion>().dataID;
        if (data.characPos.ContainsKey(playerID)) {
            posToGo = data.characPos[playerID];
            sceneToLoad = data.GetScene();
            OnLoadRequestEvent(sceneToLoad, posToGo, true);
        }
    }
}
