using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public UnityEvent item_event;
    public void Start_event()
    {
        item_event?.Invoke();
    }
}
