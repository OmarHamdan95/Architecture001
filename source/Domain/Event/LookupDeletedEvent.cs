﻿using Architecture.Domain.Common;

namespace Architecture.Domain.Event;

public class LookupDeletedEvent : BaseEvent
{
    public LookupDeletedEvent(Lookup item)
    {
        Item = item;
    }

    public Lookup Item { get; }
}
