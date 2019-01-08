using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.EventBus
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Timestamp { get; }
    }
}
