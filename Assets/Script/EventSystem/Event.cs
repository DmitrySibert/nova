using System.Collections;
using System.Collections.Generic;

public class Event {

    private string m_type;

    public Event(string type)
    {
        m_type = type;
    }

    public string Type
    {
        get { return m_type; }
    }
}
