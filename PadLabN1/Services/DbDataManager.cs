using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PadLabN1.Entities;
using PadLabN1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using PadLabN1.Controllers;
using PadLabN1.Hubs;
using Renci.SshNet.Messages.Transport;

namespace PadLabN1.Services
{
    public class DbDataManager : IDataManager
    {
        public DbDataManager
        (
            PadLabN1DbContext dbContext,
            MessageController msgController
        )
        {
            _dbContext = dbContext;
            _msgController = msgController;
        }

        private readonly PadLabN1DbContext _dbContext;
        private readonly MessageController _msgController;

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public IEnumerable<UserWithSubsDto> GetAllUsersWithSubs()
        {
            var users = _dbContext.Users.ToList();
            var usersWithSubs = Mapper.Map<IEnumerable<UserWithSubsDto>>(users);
            foreach (var user in usersWithSubs)
            {
                user.Subs = GetSubList(user.Id);
            }

            return usersWithSubs;
        }

        public bool TryDeletePost(int postId)
        {
            var post = _dbContext.Posts.FirstOrDefault(p => p.PostId == postId);
            _dbContext.Posts.Remove(post);
            if (_dbContext.SaveChanges() == 0)
            {
                return false;
            }

            return true;
        }

        public bool TryDeleteUser(int Id)
        {
            var posts = _dbContext.Posts.Where(p => p.UserId == Id).ToList();

            if (posts.Count > 0)
            {
                foreach (var post in posts)
                {
                    _dbContext.Remove(post);
                }

                if (_dbContext.SaveChanges() == 0)
                {
                    return false;
                }
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == Id);
            _dbContext.Users.Remove(user);
            if (_dbContext.SaveChanges() == 0)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<PostDto> GetPostsOfUser(int userId)
        {
            var posts = _dbContext.Posts.Where(p => p.UserId == userId).ToList();

            return Mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public IEnumerable<PostDto> GetPostsBySubs(string userName)
        {
            var posts = _dbContext.Posts
                .Where(p => p.User.Sub.Any(s => s.Subscriber.Name == userName))
                .Include(p => p.User)
                .ToList();

            return Mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public string GetAuthorName(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            return user != null ? user.Name : "";
        }


        public bool TryAddSub(SubForCreation subForCreation)
        {
            if (subForCreation == null)
            {
                return false;
            }

            var subId = _dbContext.Users.FirstOrDefault(u => u.Id == subForCreation.SubId);
            var subOnId = _dbContext.Users.FirstOrDefault(u => u.Id == subForCreation.SubOnId);
            if (subId == null || subOnId == null)
            {
                return false;
            }

            var subCheck = _dbContext.Subscriptions
                .FirstOrDefault(s => s.SubId == subForCreation.SubId && s.SubOnId == subForCreation.SubOnId);
            if (subCheck != null)
            {
                return false;
            }

            var sub = Mapper.Map<Sub>(subForCreation);

            _dbContext.Subscriptions.Add(sub);
            return _dbContext.SaveChanges() != 0;
        }

        public ICollection<int> GetSubList(int id)
        {
            return _dbContext.Subscriptions.Where(s => s.SubId == id).Select(s => s.SubOnId).ToList();
        }


        public UserDto GetUser(int Id)
        {
            var user = _dbContext.Users.Where(u => u.Id == Id).Include(u => u.Posts).FirstOrDefault();
            return Mapper.Map<UserDto>(user);
        }


        public UserDto GetUser(string userName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Name == userName);
            return Mapper.Map<UserDto>(user);
        }

        public bool TryCreatePost(PostForCreation postForCreation, int userId)
        {
            var postToAdd = new Post
            {
                Date = DateTime.Now,
                Title = postForCreation.Title,
                Body = postForCreation.Body,
                UserId = userId
            };

            try
            {
                _dbContext.Posts.Add(postToAdd);
                if (_dbContext.SaveChanges() == 0)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            _msgController.Post(postToAdd, GetAuthorName(postToAdd.UserId));
            return true;
        }


        public int GetUserId(string userName)
        {
            return _dbContext.Users.FirstOrDefault(p => p.Name == userName).Id;
        }

        public bool TryCreateUser(UserForCreation user)
        {
            try
            {
                var userToAdd = new User
                {
                    //Id = SendersDataStore.Current.Senders.Count() + 1,
                    Name = user.Name,
                    Description = user.Description
                };

                _dbContext.Users.Add(userToAdd);
                if (_dbContext.SaveChanges() == 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteSub(int subId, int subOnId)
        {
            var sub = _dbContext.Subscriptions
                .FirstOrDefault(s => s.SubId == subId && s.SubOnId == subOnId);

            if (sub == null)
            {
                return false;
            }

            _dbContext.Subscriptions.Remove(sub);

            if (_dbContext.SaveChanges() == 0)
            {
                return false;
            }

            return true;
        }
    }
}