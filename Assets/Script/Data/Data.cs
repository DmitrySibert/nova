using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Data
{
    private Dictionary<string, object> m_container;

    public Data()
    {
        m_container = new Dictionary<string, object>();
    }

    public object this[string index]
    {
        get
        {
            return m_container[index];
        }

        set
        {
            m_container[index] = value;
        }
    }

    public T Get<T>(string key)
    {
        return (T) m_container[key];
    }
}
