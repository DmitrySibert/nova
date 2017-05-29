using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class NullEvent : Event
{
    private static NullEvent holder;

    private NullEvent() : base("NullEvent", new Data()) { }

    public static NullEvent Instance()
    {
        if (holder == null) {
            holder = new NullEvent();
        }

        return holder;
    }
}
