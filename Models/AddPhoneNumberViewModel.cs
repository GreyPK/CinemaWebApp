using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "����� ��������")]
        public string Number { get; set; }
    }
}