using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PadLabN1;
using PadLabN1.Entities;
using PadLabN1.Models;
using PadLabN1.Services;

namespace PadLabN1.Controllers
{
    [Route("dummy/")]
    [ApiController]
    public class DummyController : Controller
    {
        public DummyController(IDataManager dataManager, PadLabN1DbContext database)
        {
            _database = database;
            _dataManager = dataManager;
        }

        private readonly PadLabN1DbContext _database;
        private readonly IDataManager _dataManager;

        [HttpGet("users")]
        public ActionResult<IEnumerable<string>> GetAllUsers()
        {
            return Json(_dataManager.GetAllUsers());
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



        [HttpPost("users/{id:int}/posts")]
        public ActionResult<IEnumerable<string>> CreatePost(int id, [FromBody] PostForCreation postForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_dataManager.TryCreatePost(postForCreation, id))
            {
                return Ok();
            }

            return BadRequest();
        }



    }
}

