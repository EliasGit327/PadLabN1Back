using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PadLabN1.Entities;
using PadLabN1.Services;
using Renci.SshNet.Messages;

namespace PadLabN1.Hubs
{
    public class MessageHub: Hub
    {
        private readonly IDataManager _dataManager;

        public MessageHub(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

//        public async Task Send(string message, int id)  
//        {  
//            await Clients.Group(id.ToString()).SendAsync("send", message);  
//        }  
        
        public async Task Authorize(int id)
        {
            var subList = _dataManager.GetSubList(id);

            Task.WaitAll(subList.Select(sub => 
                Groups.AddToGroupAsync(Context.ConnectionId, sub.ToString())).ToArray());

        } 
        
        public async Task Post(Post post)
        {
            var postTest = new 
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Body = post.Body,
                Date = post.Date
            };

            await Clients.All.SendAsync( "send", postTest );
        }
    }
}