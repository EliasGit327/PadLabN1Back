using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Renci.SshNet.Messages;

namespace PadLabN1.Hubs
{
    public class MessageHub: Hub
    {
        public async Task Send(string message)  
        {  
            await Clients.All.SendAsync("send", message);  
        }  
    }
}