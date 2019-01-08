using System;
using System.Collections.Generic;
using System.Text;

namespace Abbott.Tips.Framework.EventBus
{
    public interface IEventSubscriber : IDisposable
    {
        void Subscribe();
    }
}
