using System.ComponentModel.DataAnnotations;

namespace _8._Music_portal.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Логин")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
        public string? Salt { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Електронная почта ")]
        public string? email { get; set; }

        public int? Status { get; set; }
    }
}
