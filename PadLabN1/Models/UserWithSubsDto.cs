using System.Collections.Generic;
using System.Linq;

namespace PadLabN1.Models
{
    public class UserWithSubsDto
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PostDto> Posts { get; set; } 
        public ICollection<UserDto> Subcribtions { get; set; }

        public ICollection<int> Subs { get; set; }
        public int NumberOfMessages => Posts?.Count() ?? 0;
        
    }
}