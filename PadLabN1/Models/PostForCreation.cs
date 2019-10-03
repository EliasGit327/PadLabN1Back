using System;
using System.ComponentModel.DataAnnotations;

namespace PadLabN1.Models
{
    public class PostForCreation
    {
        [Required(ErrorMessage = "You should provide an title an body.")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MinLength(1)]
        [MaxLength(500)]
        public string Body { get; set; }
    }
}
