using System.Collections;
using System.Collections.Generic;

public class EventReceiver {

    private Queue<Event> m_events;

    public EventReceiver()
    {
        m_events = new Queue<Event>();
    }

    public void Receive(Event e)
    {
        m_events.Enqueue(e);
    }

    public void Clear()
    {
        m_events.Clear();
    }

    public Event Get()
    {
        if (m_events.Count > 0)
        {
            return m_events.Dequeue();
        }
        else
        {
            return Event.NewNull();
        }
    }
}
