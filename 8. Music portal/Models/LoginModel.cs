using System.ComponentModel.DataAnnotations;

namespace _8._Music_portal.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}
