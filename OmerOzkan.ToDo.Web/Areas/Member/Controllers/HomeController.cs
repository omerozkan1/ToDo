using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.BaseControllers;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(RoleInfo.Member)]
    public class HomeController : BaseIdentityController
    {
        private readonly IReportService _reportService;
        private readonly IDutyService _dutyService;
        private readonly INotificationService _notificationService;
        public HomeController(IReportService reportService, UserManager<AppUser> userManager, IDutyService dutyService, INotificationService notificationService) : base(userManager)
        {
            _reportService = reportService;
            _dutyService = dutyService;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Home;
            var user = await GetLoggedUser();
            ViewBag.ReportCount = _reportService.GetReportCountByAppUserId(user.Id.ToString());
            ViewBag.CompletedDutyCount = _dutyService.GetDutyCountCompleteByAppUserId(user.Id.ToString());
            ViewBag.ToBeCompletedDutyCount = _dutyService.GetDutyCountToBeCompletedByAppUserId(user.Id.ToString());
            ViewBag.NotReadCount = _notificationService.GetNotReadCountByAppUserId(user.Id.ToString());

            return View();
        }
    }
}
