using UnityEngine;

public class DataDefiantion : MonoBehaviour
{
    public PersistentType persistentType;
    public string dataID;

    private void OnValidate()
    {
        if(persistentType == PersistentType.READWRITE){
            if(dataID == string.Empty){
                dataID = System.Guid.NewGuid().ToString();
            }
        }
        else{
            dataID = string.Empty;
        }
    }
}