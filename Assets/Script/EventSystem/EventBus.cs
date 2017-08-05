using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour {

    public static EventBus instance;

    private Dictionary<string, List<EventReceiver>> m_receivers;
    private Queue<Event> m_events;

    private void Awake()
    {
        if (instance == null) {
            m_receivers = new Dictionary<string, List<EventReceiver>>();
            m_events = new Queue<Event>();
            Debug.Log("EventBus initialized");
            instance = this;
            
        } else if (instance != this) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        while (m_events.Count != 0)
        {
            Event evt = m_events.Dequeue();
            List<EventReceiver> evtRcvrs;
            bool res = m_receivers.TryGetValue(evt.Name, out evtRcvrs);
            if (res)
            {
                foreach (EventReceiver evtRec in evtRcvrs)
                {
                    evtRec.Receive(evt);
                }
            }
            else
            {
                Debug.LogWarning("Trigged event without receivers: " + evt.Name);
            }
        }
    }

    public void AddReceiver(string evtType, EventReceiver evtRec)
    {
        List<EventReceiver> evtRcvrs;
        bool res = m_receivers.TryGetValue(evtType, out evtRcvrs);
        if (res) {
            evtRcvrs.Add(evtRec);
        } else { 
            evtRcvrs = new List<EventReceiver>();
            evtRcvrs.Add(evtRec);
            m_receivers.Add(evtType, evtRcvrs);
        }
    }

    public void RemoveReceiver(string evtType, EventReceiver evtRec)
    {
        List<EventReceiver> evtRcvrs;
        bool res = m_receivers.TryGetValue(evtType, out evtRcvrs);
        if (res) {
            bool k = evtRcvrs.Remove(evtRec);
        }
    }

    public void TriggerEvent(Event evt)
    {
        m_events.Enqueue(evt);
    }
}
