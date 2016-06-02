using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }
}