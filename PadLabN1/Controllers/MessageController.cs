using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PadLabN1.Entities;
using PadLabN1.Hubs;
using PadLabN1.Services;


namespace PadLabN1.Controllers
{
//    [Route("api/message")]
    public class MessageController: Controller
    {
        private IHubContext<MessageHub> _messageHubContext;
        
        public MessageController( IHubContext<MessageHub> messageHubContext )
        {
            _messageHubContext = messageHubContext;
        }
        
        public async Task Post(Post post, IDataManager dmContext)
        {
            var postTest = new 
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Name = dmContext.GetAuthorName(post.UserId),
                Title = post.Title,
                Body = post.Body,
                Date = post.Date
            };
            

            await _messageHubContext.Clients.Group(post.UserId.ToString())
                .SendAsync( "send", postTest );
        }

        
    }
}