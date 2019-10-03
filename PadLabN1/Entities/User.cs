using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PadLabN1.Entities;

namespace PadLabN1.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }
               = new List<Post>();

        [InverseProperty("SubOn")]
        public ICollection<Sub> Sub { get; set; }
                = new List<Sub>();

    }
}
