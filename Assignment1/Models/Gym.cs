using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Gym
    {
        public int GymId { get; set; }
        [Required]
        [Display(Name = "Gym Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        public List<Climb> Climbs { get; set; }
    }
}
