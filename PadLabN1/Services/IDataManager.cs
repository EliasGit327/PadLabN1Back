using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PadLabN1.Models;

namespace PadLabN1.Services
{
    public interface IDataManager
    {
        int GetUserId(string userName);
        bool TryCreateUser(UserForCreation user);

        bool TryDeleteUser(int Id);

        bool TryDeleteSub( int subId, int subOnId );

        UserDto GetUser(int id);
        
        UserDto GetUser(string userName);

        IEnumerable<UserDto> GetAllUsers();
        
        IEnumerable<UserWithSubsDto> GetAllUsersWithSubs();

        bool TryCreatePost(PostForCreation postForCreation, int id);

        bool TryDeletePost(int postId);

        IEnumerable<PostDto> GetPostsOfUser(int userId);

        IEnumerable<PostDto> GetPostsBySubs(string userName);

        string GetAuthorName( int id );

        bool TryAddSub(SubForCreation subForCreation);

        ICollection<int> GetSubList( int id );
        
    }
}
