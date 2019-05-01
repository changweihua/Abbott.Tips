using Abbott.Tips.Application.TEvents;
using Abbott.Tips.Framework.EventBus;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.ApiCore.Events
{
    public class EntityFrameworkEventStore : IEventStore
    {
        private readonly IEventService iEventService;

        public EntityFrameworkEventStore(IEventService eventService)
        {
            iEventService = eventService;
        }

        public async Task SaveEventAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            await iEventService.CreateEvent(new TEventModel
            {
                CreatedBy = 1,
                EventId = @event.Id,
                EventPayload = Newtonsoft.Json.JsonConvert.SerializeObject(@event),
                EventTimestamp = @event.Timestamp
            });
        }

        #region IDisposable Support

        public void Dispose()
        {
        }

        #endregion
    }
}
