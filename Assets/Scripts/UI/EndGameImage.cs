using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class EndGameImage : MonoBehaviour
{
    public List<Image> images;
    public GameSceneSO menu;

    [Header("事件广播")]
    [SerializeField] SceneLoadEventSO endGameEvent;

    private void Update()
    {
        if(GameManager.Instance.endGame){
            EndGame();
        }
    }

    private void EndGame()
    {
        string imageNmae = string.Empty;
        switch(Player.Instance.maxHP){
            case 3:{
                    imageNmae = "Fall";
                    break;
            }
            case 4:{
                    imageNmae = "C";
                    break;
            }
            case 5:{
                    imageNmae = "B";
                    break;
            }
            case 6:{
                    imageNmae = "A";
                    break;
            }
            case 7:{
                    imageNmae = "SSS";
                    break;
            }
        }
        foreach(var image in images){
            if (image.name == imageNmae) {
                StartCoroutine(ShowImage(image));
                break;
            }
        }
    }

    private System.Collections.IEnumerator ShowImage(Image image)
    {
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        endGameEvent.RaiseLoadRequestEvent(menu, Vector3.zero, true);
        GameManager.Instance.endGame = false;
        yield return new WaitForSeconds(2);
        image.gameObject.SetActive(false);
    }
}
