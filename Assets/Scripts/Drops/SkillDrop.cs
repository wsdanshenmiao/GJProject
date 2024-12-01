using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDrop : BaseDrops
{
    [SerializeField] private List<PlayerState> states;

    [SerializeField] private Image skillImage;
    public float waitTime = 5;

    private void Start()
    {
        skillImage.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player")) {
            skillImage.enabled = true;
            foreach(var state in states){
                coll.GetComponent<PlayerStateMachine>().AddState(state.GetType(), Instantiate(state));
            }
            StartCoroutine(ShowInterface());
        }
    }

    private System.Collections.IEnumerator ShowInterface()
    {
        yield return new WaitForSeconds(waitTime);
        if(m_DropsPool){
            m_DropsPool.Release(this);
        }
        else{
            Destroy(gameObject);
        }
    }
}
