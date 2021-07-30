using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;
using OmerOzkan.ToDo.Dto.Dtos.ReportDtos;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.BaseControllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(RoleInfo.Member)]
    public class WorkOrderController : BaseIdentityController
    {
        private readonly IReportService _reportService;
        private readonly IDutyService _dutyService;
        private readonly INotificationService _notificationService;
        private readonly IGenericService<Duty> _genericDutyService;
        private readonly IGenericService<Notification> _genericNotificationService;
        private readonly IGenericService<Report> _genericReportService;
        private readonly IMapper _mapper;
        public WorkOrderController(IReportService reportService, UserManager<AppUser> userManager, IDutyService dutyService, INotificationService notificationService, IGenericService<Duty> genericDutyService, IGenericService<Notification> genericNotificationService, IGenericService<Report> genericReportService, IMapper mapper) : base(userManager)
        {
            _reportService = reportService;
            _dutyService = dutyService;
            _notificationService = notificationService;
            _genericDutyService = genericDutyService;
            _genericNotificationService = genericNotificationService;
            _genericReportService = genericReportService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.WorkOrder;

            var user = await GetLoggedUser();

            return View(_mapper.Map<List<DutyListDto>>(_dutyService.GetAll(I => I.AppUserId == user.Id.ToString() && !I.Status)));
        }

        public IActionResult AddReport(int id)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;

            var duty = _dutyService.GetByUrgencyId(id);

            ReportAddDto model = new ReportAddDto();
            model.DutyId = id;
            model.Duty = duty;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReport(ReportAddDto model)
        {
            if (ModelState.IsValid)
            {
                await _genericReportService.AddAsync(new Report()
                {
                    DutyId = model.DutyId,
                    Detail = model.Detail,
                    Description = model.Description
                });

                var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
                var activeUser = await GetLoggedUser();

                foreach (var admin in adminUserList)
                {
                    await _genericNotificationService.AddAsync(new Notification
                    {
                        Description = $"{activeUser.Name} {activeUser.SurName} yeni bir rapor yazdı",
                        AppUserId = admin.Id.ToString(),
                    });
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult UpdateReport(int id)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;

            var report = _reportService.GetByReportId(id);
            ReportUpdateDto model = new ReportUpdateDto();
            model.Id = report.Id;
            model.Description = report.Description;
            model.Detail = report.Detail;
            model.Duty = report.Duty;
            model.DutyId = report.DutyId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReport(ReportUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var updatedReport = _reportService.GetByReportId(model.Id);

                updatedReport.Description = model.Description;
                updatedReport.Detail = model.Detail;

                await _genericReportService.UpdateAsync(updatedReport);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> CompleteDuty(int dutyId)
        {
            var updatedDuty = await _genericDutyService.FindByIdAsync(dutyId);

            updatedDuty.Status = true;
            await _genericDutyService.UpdateAsync(updatedDuty);

            var adminUserList = await _userManager.GetUsersInRoleAsync("Admin");
            var activeUser = await GetLoggedUser();

            foreach (var admin in adminUserList)
            {
                await _genericNotificationService.AddAsync(new Notification
                {
                    Description = $"{activeUser.Name} {activeUser.SurName} vermiş olduğunuz bir görevi tamamladı",
                    AppUserId = admin.Id.ToString(),
                });
            }
            return Json(null);
        }
    }
}
