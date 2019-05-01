using Abbott.Tips.Framework.Dependency;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.TEvents
{
    public interface IEventService : IDependency
    {
        Task<int> CreateEvent(TEventModel @event);
    }
}
