using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Climb
    {
        public int ClimbId { get; set; }
        [Required]
        public string Colour { get; set; }
        [Required]
        public string Style { get; set; }
        [Required]
        public Boolean Completed { get; set; }
        [Required]
        public int GymId { get; set; }
        [Required]
        public Gym Gym { get; set; }
    }
}
