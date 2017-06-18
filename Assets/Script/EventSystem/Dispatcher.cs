using System;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    [SerializeField]
    private string[] m_subscribedEventTypes;

    private EventBus m_eventBus;
    private EventReceiver m_eventReceiver;

    void Awake()
    {
        m_eventBus = FindObjectOfType<EventBus>();
        m_eventReceiver = new EventReceiver();
    }

    void Start()
    {
        foreach (string evt in m_subscribedEventTypes) {
            Subscribe(evt);
        }
    }

    private void OnEnable()
    {
        m_eventReceiver.Clear();
    }

    public void Subscribe(string evtName)
    {
        m_eventBus.AddReceiver(evtName, m_eventReceiver);
    }

    public Event ReceiveEvent()
    {
        return m_eventReceiver.Get();
    }
}
