using UnityEngine;

public class Attack : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    public LayerMask attackLayer;

    protected void OnCollisionStay2D(Collision2D coll)
    {
        //Debug.Log("CollisionEnter");
        if (IsInLayerMask(coll.gameObject, attackLayer)){
            coll.collider.GetComponent<Character>()?.TakeDamege(this);
        }
    }

    protected void OnTriggerStay2D(Collider2D coll)
    {
        //Debug.Log("TriggerEnter");
        if (IsInLayerMask(coll.gameObject, attackLayer)){
            coll.GetComponent<Character>()?.TakeDamege(this);
        }
    }

    public float CurrentDamege(Character character)
    {
        return Mathf.Max(0, Random.Range(minDamage,maxDamage) - character.defence);
    }

    public bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // 根据Layer数值进行移位获得用于运算的Mask值
        int objLayerMask = 1 << obj.layer;
        return (layerMask.value & objLayerMask) > 0;
    }
}
