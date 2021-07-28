using System.ComponentModel.DataAnnotations;

namespace OmerOzkan.ToDo.Dto.Dtos.AppUserDtos
{
    public class AppUserLoginDto
    {

        [Required(ErrorMessage = "Email boş geçilemez.")]
        [Display(Name = "Emailiniz: ")]
        [EmailAddress(ErrorMessage = "Lütfen email bilginizi kontrol ediniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parola boş geçilemez.")]
        [Display(Name = "Parolanız: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla ")]
        public bool RememberMe { get; set; }
    }
}
