using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "�����")]
        public string Email { get; set; }
    }
}