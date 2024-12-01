using UnityEngine;

public class Portal : MonoBehaviour , IInteractable
{
    [Header("事件广播")]
    [SerializeField] SceneLoadEventSO loadEvent;

    [SerializeField]GameSceneSO sceneToGO;
    public Vector3 nextPortal;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(nextPortal, 1f);
    }
        
    public void TriggerAction()
    {
        loadEvent.RaiseLoadRequestEvent(sceneToGO, nextPortal, true);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        TriggerAction();
    }

}
