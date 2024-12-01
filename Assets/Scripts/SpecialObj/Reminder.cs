using UnityEngine;

public class Reminder : MonoBehaviour
{
    public GameObject child;

    private void Awake()
    {
        child.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        child.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        child.SetActive(false);
    }

}
