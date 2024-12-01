using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SavePoint : MonoBehaviour ,IInteractable
{
    [SerializeField] private VoidEventSO saveDataEvent;
    [SerializeField] private Sprite unactiveSprite;
    [SerializeField] private Sprite activeSprite;

    new private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private Light2D savePointLight;
    private bool isActive = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        savePointLight = GetComponentInChildren<Light2D>();
        spriteRenderer.sprite = unactiveSprite;
    }

    private void OnEnable()
    {
        spriteRenderer.sprite = isActive ? activeSprite : unactiveSprite;
        savePointLight.gameObject.SetActive(isActive);
    }

    public void TriggerAction()
    {
        if (isActive) return;

        isActive = true;
        spriteRenderer.sprite = activeSprite;
        savePointLight.gameObject.SetActive(isActive);
        // 保存数据
        saveDataEvent.RaiseEvent();
        // collider.enabled = false;
        // gameObject.tag = "Untagged";
    }


}