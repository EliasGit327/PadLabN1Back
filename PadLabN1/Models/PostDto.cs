using System;
namespace PadLabN1.Models
{
    public class PostDto
    {
        //public UserDto User { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
