using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TES_MEDICAL.SHARE.Models.ViewModel;

namespace TES_MEDICAL.GUI.Infrastructure
{
    public class DoctorHub:Hub
    {
        public async Task Sendoctor(STTViewModel data, string connectionId)
        => await Clients.Client(connectionId).SendAsync("broadcastchartdata", data);
        public string GetConnectionId() => Context.ConnectionId;
    }
}
