using System;
using System.Collections;
using System.Collections.Generic;

public class EventPool {

    private Dictionary<string, Func<Event>> m_evtCreators;

    public EventPool()
    {
        m_evtCreators = new Dictionary<string, Func<Event>>();
        m_evtCreators["LeftMouseUp"] = () =>
        {
            Event evt = new Event("LeftMouseUp");
            return evt;
        };
        m_evtCreators["RightMouseUp"] = () =>
        {
            Event evt = new Event("LeftMouseUp");
            return evt;
        };
        m_evtCreators["SupernovaShoot"] = () =>
        {
            Event evt = new Event("SupernovaShoot");
            return evt;
        };
    }

    public Event GetEvent(string evtType)
    {
        return m_evtCreators[evtType].Invoke();
    }
}
