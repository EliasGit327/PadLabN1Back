using System.ComponentModel.DataAnnotations;

namespace PadLabN1.Models
{
    public class SubForCreation
    {
        [Required(ErrorMessage = "You should provide SubId.")]
        public int SubId { get; set; }
        [Required(ErrorMessage = "You should provide SubOnId.")]
        public int SubOnId { get; set; }
    }
}