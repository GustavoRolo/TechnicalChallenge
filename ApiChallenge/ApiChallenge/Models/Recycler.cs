using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Models
{
    public class Recycler
    {
        [Key]
        [Required]
        public Guid id { get; set; }
        [Required]
        public bool run { get; set; }
        [Required]
        public int days { get; set; }
    }
}
