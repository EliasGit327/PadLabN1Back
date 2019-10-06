using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PadLabN1;
using PadLabN1.Entities;
using PadLabN1.Hubs;
using PadLabN1.Models;
using PadLabN1.Services;

namespace PadLabN1.Controllers
{
    [Route("api/")]
    [ApiController]
    public class MainController : Controller
    {
        public MainController
        (
            IDataManager dataManager,
            PadLabN1DbContext database,
            IHubContext<MessageHub> messageHubContext
        )
        {
            _database = database;
            _dataManager = dataManager;
            _messageHubContext = messageHubContext;
        }

        private readonly PadLabN1DbContext _database;
        private readonly IDataManager _dataManager;
        private IHubContext<MessageHub> _messageHubContext;

        [HttpGet("users")]
        public ActionResult<IEnumerable<string>> GetAllUsers()
        {
            var users = _dataManager.GetAllUsers();

            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet("userswithsubs")]
        public ActionResult<IEnumerable<string>> GetAllUsersWithSubs()
        {
            var users = _dataManager.GetAllUsersWithSubs();

            if (users == null)
                return NotFound();

            return Ok(users);
        }


        [HttpGet("userswithsubs/{userName}")]
        public ActionResult<IEnumerable<string>> GetUserWithSubs(string userName)
        {
            var user = _dataManager.GetAllUsersWithSubs().FirstOrDefault(u => u.Name == userName);


            if (user == null)
                return NotFound();

            return Ok(user);
        }


        [HttpGet("users/{id:int}")]
        public ActionResult<IEnumerable<string>> GetSpecificUser(int id)
        {
            var user = _dataManager.GetUser(id);

            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest();
        }

        [HttpGet("subs/{userName}")]
        public ActionResult<IEnumerable<string>> GetPostsBySubs(string userName)
        {
            var posts = _dataManager.GetPostsBySubs(userName);

            if (posts != null)
            {
                return Ok(posts);
            }

            return BadRequest();
        }

        [HttpGet("userbyname/{userName}")]
        public ActionResult<IEnumerable<string>> GetUserByName(string userName)
        {
            var posts = _dataManager.GetUser(userName);

            if (posts != null)
            {
                return Ok(posts);
            }

            return NotFound();
        }


        [HttpGet("users/{id:int}/posts")]
        public ActionResult<IEnumerable<string>> GetPosts(int id)
        {
            var posts = _dataManager.GetPostsOfUser(id);

            if (posts != null)
            {
                return Ok(posts);
            }

            return BadRequest();
        }


        [HttpPost("users")]
        public ActionResult<IEnumerable<string>> RegisterUser([FromBody] UserForCreation user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataManager.TryCreateUser(user))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("posts/{postId:int}")]
        public ActionResult<IEnumerable<string>> DeletePost(int postId)
        {
            if (postId == null)
            {
                return BadRequest();
            }

            if (_dataManager.TryDeletePost(postId))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("users/{postId:int}")]
        public ActionResult<IEnumerable<string>> DeleteUser(int postId)
        {
            if (postId == null)
            {
                return BadRequest();
            }

            if (_dataManager.TryDeleteUser(postId))
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpGet("users/{id:int}/name")]
        public ActionResult<IEnumerable<string>> GetUserName(int id)
        {
            var name = _dataManager.GetAuthorName(id);
            if (name != null)
            {
                return Json(name);
            }

            return NotFound();
        }

        [HttpGet("subs/{id:int}")]
        public ActionResult<IEnumerable<string>> GetSubList(int id)
        {
            var subs = _dataManager.GetSubList(id);
            if (subs != null)
            {
                return Json(subs);
            }

            return NotFound();
        }


        [HttpPost("users/{name}/posts")]
        public ActionResult<IEnumerable<string>> CreatePost(string name, [FromBody] PostForCreation postForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idOfUser = _dataManager.GetUserId(name);
            if (_dataManager.TryCreatePost(postForCreation, idOfUser))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("subs")]
        public ActionResult<IEnumerable<string>> CreatePost([FromBody] SubForCreation subForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataManager.TryAddSub(subForCreation))
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpDelete("subs/{subId:int}/{subOnId:int}")]
        public ActionResult<IEnumerable<string>> DeleteSub(int subId, int subOnId)
        {
            if (subId == null || subOnId == null)
            {
                return BadRequest();
            }

            if (_dataManager.TryDeleteSub(subId, subOnId))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}