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
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public Wrapper(UserManager<AppUser> userManager, INotificationService notificationService, IMapper mapper)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var identityUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var model = _mapper.Map<AppUserDto>(identityUser);

            var notifications = _notificationService.GetNotReadUsers(model.Id).Count;
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
