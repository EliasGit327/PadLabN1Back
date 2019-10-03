using System;
using System.Collections.Generic;
using System.Linq;
namespace PadLabN1.Models
{
    public class UserDto
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PostDto> Posts { get; set; } 
        public ICollection<UserDto> Subcribtions { get; set; }

        public int NumberOfMessages => Posts?.Count() ?? 0;
        //public int NumberOfMessages => Posts.Count();
    }
}
