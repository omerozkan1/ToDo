using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.NotificationDtos;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.BaseControllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(RoleInfo.Admin)]
    public class NotificationController : BaseIdentityController
    {
        private readonly IGenericService<Notification> _genericNotificationService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService, IGenericService<Notification> genericNotificationService, IMapper mapper, IAppUserService appUserService) : base(appUserService)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _genericNotificationService = genericNotificationService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Notification;
            var user = await GetLoggedUser();
            return View(_mapper.Map<List<NotificationListDto>>(_notificationService.GetNotReadUsers(user.Id.ToString())));
        }

        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            var updatedNotification = await _genericNotificationService.FindByIdAsync(id);
            updatedNotification.Status = true;
            await _genericNotificationService.UpdateAsync(updatedNotification);
            return RedirectToAction("Index");
        }
    }
}
