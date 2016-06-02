using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "����� ����������� �����")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "������")]
        public string Password { get; set; }

        [Display(Name = "��������� ����")]
        public bool RememberMe { get; set; }
    }
}