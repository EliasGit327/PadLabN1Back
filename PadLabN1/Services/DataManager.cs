using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PadLabN1.Models;

namespace PadLabN1.Services
{
    public class DataManager 
    {
        public IEnumerable<UserDto> GetAllUsers()
        {
            return SendersDataStore.Current.Senders;
        }

        public int GetUserId( string userName )
        {
            throw new NotImplementedException( );
        }

        public bool TryCreateUser(UserForCreation user)
        {
            try
            {
                var userDto = new UserDto
                {
                    Id = SendersDataStore.Current.Senders.Count() + 1,
                    Name = user.Name,
                    Description = user.Description
                };
                SendersDataStore.Current.Senders.Add(userDto);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool TryCreatePost(PostForCreation postForCreation, int userId)
        {
            if (postForCreation == null)
            {
                return false;
            }

            var user = SendersDataStore.Current.Senders.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                Console.WriteLine(userId);
                return false;
            }

            var newPost = new PostDto
            {
                //PostId = user.Posts.Count + 1,
                Date = DateTime.Now,
                Title = postForCreation.Title,
                Body = postForCreation.Body
            };

            SendersDataStore.Current.Senders.FirstOrDefault( u => u.Id == userId).Posts.Add(newPost);
            return true;
            
        }

        public bool TryDeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public IActionResult DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostDto> GetPostsOfUser(int userId)
        {
            return SendersDataStore.Current.Senders.FirstOrDefault(u => u.Id == userId).Posts;
        }

        public IEnumerable<PostDto> GetPostsOfUsers(int[] usersId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostDto> GetPostsBySubs(string userName)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUser(int Id)
        {
            return SendersDataStore.Current.Senders.FirstOrDefault(u => u.Id == Id);
        }
    }
}
