using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "����� ����������� �����")]
        public string Email { get; set; }
    }
}