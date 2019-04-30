using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abbott.Tips.WebHost.Hubs
{
    //[Authorize]
    public class TestHub : Hub
    {
        public async Task GetLastestCount(string random)
        {
            int count = 10;
            //var userName = Context.User.Identity.Name;
            do
            {
                await Clients.All.SendAsync("ReceiveUpdate", random);
            } while (count < 10);
            await Clients.All.SendAsync("Finished");
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("someFunc", new { random = DateTime.Now });
            await Clients.AllExcept(connectionId).SendAsync("someFunc", new { random = DateTime.Now });

            await Groups.AddToGroupAsync(connectionId, "MyGroup");
            await Groups.RemoveFromGroupAsync(connectionId, "MyGroup");

            await Clients.Group("MyGroup").SendAsync("someFunc", new { random = DateTime.Now });

            //return base.OnConnectedAsync();
        }

    }
}
