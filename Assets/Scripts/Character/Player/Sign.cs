using Unity.Mathematics;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public GameObject signSprite;
    
    private bool canPress = false;
    private IInteractable interactableObj;

    private void Awake()
    {
        interactableObj = null;
    }

    private void Update()
    {
        signSprite.SetActive(canPress);
        if (!canPress) return;

        // 修正缩放
        Vector3 scale = transform.localScale;
        transform.localScale = Player.Instance.transform.localScale.x < 0 ? 
            new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z) : 
            new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
        if(Player.Instance.GetComponent<PlayerInput>().isConfirm){
            // 触发确认事件
            interactableObj?.TriggerAction();
        }
    }

    // 碰到交互物体
    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Interactable")){
            canPress = true;
            interactableObj = coll.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Interactable")) {
            canPress = false;
        }
    }
}