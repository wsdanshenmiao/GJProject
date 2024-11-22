using UnityEngine;

[CreateAssetMenu(menuName = "Data/CharacterDataSO")]
public class CharacterStatsSO : ScriptableObject
{
    public float m_MaxHealth;
    public float m_CurrentHealth;
    public float m_MaxSpeed;
    public float m_CurrentSpeed;
}
