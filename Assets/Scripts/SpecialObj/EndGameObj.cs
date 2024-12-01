using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameObj : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    public GameObject text;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        text.SetActive(false);
    }

    public void TriggerAction()
    {
        GameManager.Instance.endGame = true;
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Player")){
            spriteRenderer.enabled = true;
            text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("Player")){
            spriteRenderer.enabled = false;
            text.SetActive(false);
        }
    }
}
