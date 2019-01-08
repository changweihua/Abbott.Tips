using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.EventBus
{
    public interface IEventBus : IEventPublisher, IEventSubscriber { }

    public class EventProcessedEventArgs : EventArgs
    {
        public EventProcessedEventArgs(IEvent @event)
        {
            this.Event = @event;
        }

        public IEvent Event { get; }
    }
}
