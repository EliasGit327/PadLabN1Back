using System;
using System.ComponentModel.DataAnnotations;

namespace PadLabN1.Models
{
    public class UserForCreation
    {
        [Required(ErrorMessage = "You should provide an title an body.")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MinLength(1)]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
