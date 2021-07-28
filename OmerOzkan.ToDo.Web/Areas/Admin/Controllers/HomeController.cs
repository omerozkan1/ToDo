﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.BaseControllers;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(RoleInfo.Admin)]
    public class HomeController : BaseIdentityController
    {
        private readonly IDutyService _dutyService;
        private readonly INotificationService _notificationService;
        private readonly IReportService _reportService;

        public HomeController(IDutyService dutyService, INotificationService notificationService, IReportService reportService, UserManager<AppUser> userManager) : base(userManager)
        {
            _dutyService = dutyService;
            _notificationService = notificationService;
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Home;

            var user = await GetLoginUser();

            ViewBag.AtanmayiBekleyenGorevSayisi = _dutyService.GetDutyCountPendingAssignment();
            ViewBag.TamamlanmisGorevSayisi = _dutyService.GetDutyCountCompleted();
            ViewBag.OkunmamisBildirimSayisi = _notificationService.GetNotReadCountByAppUserId(user.Id.ToString());
            ViewBag.ToplamRaporSayisi = _reportService.GetReportCount();

            return View();
        }
    }
}