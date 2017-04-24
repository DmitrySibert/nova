using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    private EventBus m_eventBus;
    private EventReceiver m_eventReceiver;

    void Awake()
    {
        m_eventReceiver = new EventReceiver();
    }

    void Start()
    {
        m_eventBus = FindObjectOfType<EventBus>();
    }

    public void Subscribe(string evtType)
    {
        m_eventBus.AddReceiver(evtType, m_eventReceiver);
    }

    public void TriggerEvent(string type)
    {
        m_eventBus.TriggerEvent(m_eventBus.GetEvent(type));
    }

    public Event ReceiveEvent()
    {
        return m_eventReceiver.Get();
    }
}
