using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitialLoad : MonoBehaviour
{
    [SerializeField] AssetReference persistantScene;

    private void Awake()
    {
        Addressables.LoadSceneAsync(persistantScene);
    }
}
