using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> onEventRaised;

    public void RaiseEvent(Character character)
    {
        onEventRaised?.Invoke(character);
    } 
}
