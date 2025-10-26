using UnityEngine;

public class UIhide : MonoBehaviour
{
    public float hide_time;
    void Start()
    {
        Invoke("Hide_self", hide_time);
    }

    void Hide_self()
    {
        this.gameObject.SetActive(false);
    }
}
