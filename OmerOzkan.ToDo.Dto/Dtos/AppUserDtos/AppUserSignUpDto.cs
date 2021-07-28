using System.ComponentModel.DataAnnotations;

namespace OmerOzkan.ToDo.Dto.Dtos.AppUserDtos
{
    public class AppUserSignUpDto
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez.")]
        [Display(Name = "Kullanıcı Adınız: ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola boş geçilemez.")]
        [Display(Name = "Parolanız: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Email boş geçilemez.")]
        [Display(Name = "Emailiniz: ")]
        [EmailAddress(ErrorMessage = "Lütfen email bilginizi kontrol ediniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ad boş geçilemez.")]
        [Display(Name = "Adınız: ")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Soyad boş geçilemez.")]
        [Display(Name = "Soyadınız: ")]
        public string Surname { get; set; }
    }
}
