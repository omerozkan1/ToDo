using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.BaseControllers;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(RoleInfo.Member)]
    public class UserController : BaseIdentityController
    {
        private readonly IMapper _mapper;
        public UserController(UserManager<AppUser> userManager, IMapper mapper) : base(userManager)
        {
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Profile;
            var appUser = await GetLoggedUser();

            return View(_mapper.Map<AppUserDto>(appUser));
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserDto model, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = _userManager.Users.FirstOrDefault(I => I.Id == Convert.ToInt32(model.Id));
                if (image != null)
                {
                    string extension = Path.GetExtension(image.FileName);
                    string imageName = Guid.NewGuid() + extension;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/" + imageName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    updatedUser.Picture = imageName;
                }

                updatedUser.Name = model.Name;
                updatedUser.SurName = model.SurName;
                updatedUser.Email = model.Email;

                var result = await _userManager.UpdateAsync(updatedUser);

                if (result.Succeeded)
                {
                    TempData["message"] = "Güncelleme işleminiz başarı ile gerçekleşti.";
                    return RedirectToAction("Index");
                }
                AddError(result.Errors);
            }
            return View(model);
        }
    }
}
