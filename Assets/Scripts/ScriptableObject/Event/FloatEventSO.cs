using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FloatEventSO")]
public class FloatEventSO : ScriptableObject
{
    public UnityAction<float> onEventRaised;
    
    public void RaiseEvent(float value)
    {
        onEventRaised?.Invoke(value);
    } 

}
