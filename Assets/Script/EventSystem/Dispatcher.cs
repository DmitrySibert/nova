using System;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    [SerializeField]
    private string[] subscribedEventNames;

    private EventBus eventBus;
    private EventReceiver eventReceiver;

    void Awake()
    {
        eventBus = FindObjectOfType<EventBus>();
        eventReceiver = new EventReceiver();
    }

    void Start()
    {
        foreach (string evtName in subscribedEventNames) {
            eventBus.AddReceiver(evtName, eventReceiver);
        }
    }

    private void OnEnable()
    {
        eventReceiver.Clear();
    }

    public Event ReceiveEvent()
    {
        return eventReceiver.Get();
    }

    private void OnDestroy()
    {
        foreach (string evtName in subscribedEventNames) {
            eventBus.RemoveReceiver(evtName, eventReceiver);
        }
    }
}
