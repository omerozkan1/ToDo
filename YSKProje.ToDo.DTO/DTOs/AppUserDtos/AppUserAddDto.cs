using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AppUserAddDto
    {
        //[Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        //[Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        //[Display(Name = "Parola :")]
        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "Parola alanı boş geçilemez")]
        public string Password { get; set; }

        //[Display(Name = "Parolanızı Tekrar Giriniz :")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Email :")]
        //[EmailAddress(ErrorMessage = "Geçersiz email")]
        //[Required(ErrorMessage = "Email alanı boş geçilemez")]
        public string Email { get; set; }

        //[Display(Name = "Adınız :")]
        //[Required(ErrorMessage = "Ad alanı boş geçilemez")]
        public string Name { get; set; }

        //[Display(Name = "Soyadınız :")]
        //[Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        public string Surname { get; set; }
    }
}
