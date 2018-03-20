using System.Collections;
using System.Collections.Generic;

public class Event {

    private string name;
    private Data data;

    public Event(string name)
    {
        this.name = name;
        this.data = new Data();
    }

    public Event(string name, Data data)
    {
        this.name = name;
        this.data = data;
    }

    public string Name
    {
        get { return name; }
    }

    public Data Data
    {
        get { return data; }
        set { data = value; }
    }

    public static Event NewNull()
    {
        return NullEvent.Instance();
    }

    public override bool Equals(object obj)
    {
        var @event = obj as Event;
        return @event != null &&
               name == @event.name;
    }

    public override int GetHashCode()
    {
        return 363513814 + EqualityComparer<string>.Default.GetHashCode(name);
    }
}
