using Abbott.Tips.EntityFrameworkCore.UnitOfWork;
using Abbott.Tips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.Application.TEvents
{
    public class EventService : IEventService
    {
        public IUnitOfWork unitOfWork { get; set; }

        public async Task<int> CreateEvent(TEventModel @event)
        {
            await unitOfWork.GetRepository<TEventModel>().InsertAsync(@event);

            return await unitOfWork.SaveChangesAsync();
        }
    }
}
