using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.Web.ViewComponents
{
    public class Wrapper : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public Wrapper(UserManager<AppUser> userManager, INotificationService notificationService, IMapper mapper, IAppUserService appUserService)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _userManager = userManager;
            _appUserService = appUserService;
        }

        public IViewComponentResult Invoke()
        {
            var identityUser = _appUserService.FindByNameAsync(User.Identity.Name).Result;
            var model = _mapper.Map<AppUserDto>(identityUser);

            var notifications = _notificationService.GetNotReadUsers(model != null ? model.Id : "0").Count;
            ViewBag.NotificationCount = notifications;

            var roles = _userManager.GetRolesAsync(identityUser).Result;

            if (roles.Contains("Admin"))
            {
                return View(model);
            }
            return View("Member", model);
        }
    }
}
