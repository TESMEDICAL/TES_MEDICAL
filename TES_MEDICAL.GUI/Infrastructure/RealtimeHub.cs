using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.GUI.Infrastructure
{
    public class RealtimeHub: Hub
    {
        public async Task SendMessage(string id,STTViewModel stt)
        {
            await Clients.All.SendAsync("ReceiveMessage", id, stt);
        }
    }
}
