using System.ComponentModel.DataAnnotations;

namespace OmerOzkan.ToDo.Dto.Dtos.AppUserDtos
{
    public class AppUserLoginDto
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez.")]
        [Display(Name = "Kullanıcı Adınız: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola boş geçilemez.")]
        [Display(Name = "Parolanız: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla ")]
        public bool RememberMe { get; set; }
    }
}
