using UnityEngine;

public abstract class BaseDrops : MonoBehaviour
{
    public DropsData DropsData;
    public uint DropCount = 1;

    protected DropsPool m_DropsPool;

    protected virtual void Awake()
    {
        m_DropsPool = GetComponentInParent<DropsPool>();
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
