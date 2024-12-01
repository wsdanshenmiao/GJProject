using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedhealthLimit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player")){
            ++Player.Instance.maxHP;
            string id = Player.Instance.GetDataID().dataID;
            DataManager.Instance.FixMaxHP(id, Player.Instance.maxHP);
            Destroy(gameObject);
        }
    }
}
