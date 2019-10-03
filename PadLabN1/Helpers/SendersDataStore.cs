using System;
using System.Collections.Generic;
using PadLabN1.Models;

namespace PadLabN1
{
    public class SendersDataStore
    {
        public static SendersDataStore Current { get; } = new SendersDataStore();

        public List<UserDto> Senders { get; set; }


        public SendersDataStore()
        {
            Senders = new List<UserDto>()
            {
                new UserDto()
                {
                    Id = 1,
                    Name = "John Doe",
                    Posts = new List<PostDto>
                    {
                        new PostDto
                        {
                            PostId = 1,
                            Title = "First message of John Doe",
                            Body = "Text for first message",
                            Date = new DateTime(2015, 1, 1, 11, 12, 11)
                        },
                        new PostDto
                        {
                            PostId = 2,
                            Title = "Second message of John Doe",
                            Body = "Text for second message"
                        },
                         new PostDto
                        {
                            PostId = 3,
                            Title = "Third message of John Doe",
                            Body = "Text for third message"
                        }
                    }
                },

                new UserDto()
                {
                    Id = 2,
                    Name = "Jon Stewart",
                    Posts = new List<PostDto>
                    {
                        new PostDto
                        {
                            PostId = 4,
                            Title = "First message of Jon Stewart",
                            Body = "The Internet is just a world passing around notes in a classroom."
                        },
                        new PostDto
                        {
                            PostId = 5,
                            Title = "Second message of Jon Stewart",
                            Body = "Ahh, Earth Day, the only day of the year where being able to hacky-sack will get you laid."
                        }
                    }
                },

                new UserDto()
                {
                    Id = 3,
                    Name = "Hideo Kojima",
                    Posts = new List<PostDto>
                    {
                        new PostDto
                        {
                            PostId = 6,
                            Title = "First message of the Genious",
                            Body = "12/10"
                        }
                    }
                }

            };

        }


    }
}
