using UnityEngine;

public class UIhide : MonoBehaviour
{
    public float hide_time;
    private void OnEnable()
    {
        Invoke("Hide_self", hide_time);
    }

    void Hide_self()
    {
        this.gameObject.SetActive(false);
    }
}
