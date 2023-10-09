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
        public int Grade { get; set; }
        
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "Completion Date")]
        public DateTime? CompletionDate { get; set; }
        
        [Required]
        [Display(Name = "Gym Name")]
        public int GymId { get; set; }

        
        public Gym? Gym { get; set; }
    }
}
