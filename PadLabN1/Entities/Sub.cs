using System.ComponentModel.DataAnnotations.Schema;

namespace PadLabN1.Entities
{
    public class Sub
    {
        public int SubId { get; set; }

        [ForeignKey("SubId")]
        public User Subscriber { get; set; }

        public int SubOnId { get; set; }

        [ForeignKey("SubOnId")]
        public User SubOn { get; set; }
    }
}
