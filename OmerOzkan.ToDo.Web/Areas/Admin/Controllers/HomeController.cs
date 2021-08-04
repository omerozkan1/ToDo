using Microsoft.AspNetCore.Authorization;
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
        private UserManager<AppUser> _userManager;

        public HomeController(IDutyService dutyService, INotificationService notificationService, IReportService reportService, UserManager<AppUser> userManager, IAppUserService appUserService) : base(appUserService)
        {
            _dutyService = dutyService;
            _notificationService = notificationService;
            _reportService = reportService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Home;

            var user = await GetLoggedUser();

            ViewBag.PendingAssignmentDutyCount = _dutyService.GetDutyCountPendingAssignment();
            ViewBag.CompletedDutyCount = _dutyService.GetDutyCountCompleted();
            ViewBag.NotReadNotificationCount = _notificationService.GetNotReadCountByAppUserId(user != null ? user.Id.ToString() : "");
            ViewBag.ReportCount = _reportService.GetReportCount();

            return View();
        }
    }
}
