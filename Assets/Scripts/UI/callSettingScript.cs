using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callSettingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerStateBar;
    [SerializeField] private GameObject setting;
    [SerializeField] private Esc inputActions;
    private bool isSettingOpen = false;
    private bool isEscOpen = false;
    void Start()
    {
        inputActions = new Esc();
        inputActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
        isEscOpen = inputActions.callSetting.Esc.WasPressedThisFrame();
        //Debug.Log("执行Update" + isEscOpen);
        if(isEscOpen)
        {
            //Debug.Log("执行Esc");
            if (isSettingOpen)
            {
                isSettingOpen = false;
                setting.SetActive(isSettingOpen);
                playerStateBar.SetActive(true);
            }
            else
            {
                isSettingOpen = true;
                setting.SetActive(isSettingOpen);
                playerStateBar.SetActive(false);
            }
        }
    }
}
