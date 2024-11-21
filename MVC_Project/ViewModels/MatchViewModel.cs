using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModels
{
    public class MatchViewModel
    {




        public string Name { get; set; }
        [Required, MinLength(3, ErrorMessage = "Team name must be at least 3 characters")]
        public string TeamA { get; set; }
        [Required, MinLength(3, ErrorMessage = "Team name must be at least 3 characters")]
        public string TeamB { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required, Range(1, 100000, ErrorMessage = "Number of available tickets must be positive")]
        public int AvailableTickets { get; set; }
        [Required]
        public string Location { get; set; }


    }
}
