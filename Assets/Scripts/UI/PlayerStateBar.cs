using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerStateBar : MonoBehaviour
{
    public float intervalScale = 0;

    [SerializeField] private Image healthPrefab;
    [SerializeField] private RectTransform parent;
    private List<Image> healthImages = new List<Image>();
    private bool isInit = false;

    private void InitBar(Character character)
    {
        healthImages.Clear();
        for(int i = 0;i < character.maxHP; ++i){
            var image = Instantiate(healthPrefab);
            image.gameObject.SetActive(true);
            image.enabled = true;
            image.transform.SetParent(parent, false);
            var rect = image.rectTransform;
            Vector3 pos = rect.localPosition;
            rect.localPosition = new Vector3(pos.x + i * rect.rect.width * intervalScale, pos.y, pos.z);
            var hp = image.GetComponentsInChildren<Image>();
            foreach(var h in hp){
                if(h.name != image.name){
                    healthImages.Add(h);
                }
            }
        }
    }

    public void OnHealthChange(Character character)
    {
        if(!isInit || healthImages.Count != character.maxHP){
            isInit = true;
            InitBar(character);
            return;
        }
        for(int i = 0; i < healthImages.Count; ++i){
            healthImages[i].enabled = i < character.currentHP ? true : false;
        }
    }
}
