using Abbott.Tips.Framework.EventBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abbott.Tips.ApiCore.Events
{
    public class UserLoginedEvent : IEvent
    {
        public UserLoginedEvent(string loginName)
        {
            this.Id = Guid.NewGuid();
            this.Timestamp = DateTime.UtcNow;
            this.LoginName = loginName;
        }

        public Guid Id { get; }

        public DateTime Timestamp { get; }

        public string LoginName { get; }
    }

    public class UserLoginedEventHandler : IEventHandler<UserLoginedEvent>
    {
        private readonly IEventStore eventStore;

        public UserLoginedEventHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public bool CanHandle(IEvent @event)
            => @event.GetType().Equals(typeof(UserLoginedEvent));

        public async Task<bool> HandleAsync(UserLoginedEvent @event, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.eventStore.SaveEventAsync(@event);
            return await Task.FromResult(true);
        }

        public Task<bool> HandleAsync(IEvent @event, CancellationToken cancellationToken = default(CancellationToken))
            => CanHandle(@event) ? HandleAsync((UserLoginedEvent)@event, cancellationToken) : Task.FromResult(false);
    }
}