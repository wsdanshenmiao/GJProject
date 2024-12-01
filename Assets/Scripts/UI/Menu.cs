using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject playerStateBar;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject setting;
    [SerializeField] private GameObject volume;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject keyboardSetting;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void ExitGame()
    {
        //Debug.Log("Exit");
        Application.Quit();
    }

    public void EnableSetting()
    {
        //Debug.Log("Settint");
        //menu.SetActive(false);
        playerStateBar?.SetActive(false);
        setting.SetActive(true);
    }

    public void EnableVolume()
    {
        setting.SetActive(false);
        volume.SetActive(true);
    }

    public void EnableKey()
    {
        setting.SetActive(false);
        keyboardSetting.SetActive(true);
    }

    public void ExitKey()
    {
        keyboardSetting.SetActive(false);
        setting.SetActive(true);
    }

    public void ExitVolume()
    {
        volume.SetActive(false);
        setting.SetActive(true);
    }

    public void ExitSetting()
    {
        setting.SetActive(false);
        playerStateBar?.SetActive(true);
    }
}
