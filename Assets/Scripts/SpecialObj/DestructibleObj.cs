using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField] private AudioDefinition hurtAudio;
    [SerializeField] private AudioDefinition deadAusio;
    [SerializeField]private GameObject effect;
    private Character characterStats;

    

    private void Awake()
    {
        characterStats = GetComponent<Character>();
    }

    private void Update()
    {
        deadAusio.enabled = false;
        hurtAudio.enabled = false;
        if(characterStats.isDead){
            deadAusio.enabled = true;
            if(effect){
                Instantiate(effect,transform.position,transform.rotation);
            }
            Destroy(gameObject);
        }
        else if(characterStats.isHurt){
            hurtAudio.enabled = true;
            characterStats.isHurt = false;
        }
    }
}
