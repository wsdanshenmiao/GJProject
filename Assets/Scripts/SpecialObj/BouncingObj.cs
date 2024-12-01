using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingObj : MonoBehaviour
{
    [SerializeField] private AudioDefinition bouceAudio;
    [SerializeField] private float bounceFactor = 10;
    public float playAudioTime = 2;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.collider.CompareTag("Player")) return;
        Rigidbody2D playerRB = coll.collider.GetComponent<Rigidbody2D>();
        playerRB.velocity = Vector2.zero;
        playerRB.AddForce(Vector2.up * bounceFactor, ForceMode2D.Impulse);
        StartCoroutine(PlayAutio());
        bouceAudio.enabled = false;
    }

    private IEnumerator PlayAutio()
    {
        bouceAudio.enabled = true;
        yield return new WaitForSeconds(playAudioTime);
        bouceAudio.enabled = false;
    }
}
