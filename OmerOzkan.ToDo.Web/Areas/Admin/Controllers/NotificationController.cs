using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.NotificationDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(RoleInfo.Admin)]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Notification;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(_mapper.Map<List<NotificationListDto>>(_notificationService.GetNotReadUsers(user.Id.ToString())));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var updatedNotification = _notificationService.FindByIdAsync(id);
            updatedNotification.Result.Status = true;
            await _notificationService.UpdateAsync(updatedNotification.Result);
            return RedirectToAction("Index");
        }
    }
}
