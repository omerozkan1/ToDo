using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;
using OmerOzkan.ToDo.Dto.Dtos.ReportDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(RoleInfo.Admin)]
    public class WorkOrderController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IDutyService _dutyService;
        private readonly IGenericService<Urgency> _urgencyService;
        private readonly IGenericService<Duty> _genericDutyService;
        private readonly IGenericService<Notification> _genericNotificationService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public WorkOrderController(IAppUserService appUserService, IDutyService dutyService, IGenericService<Urgency> urgencyService, IGenericService<Duty> genericDutyService, IGenericService<Notification> genericNotificationService, UserManager<AppUser> userManager, IFileService fileService, INotificationService notificationService, IMapper mapper)
        {
            _appUserService = appUserService;
            _dutyService = dutyService;
            _genericDutyService = genericDutyService;
            _genericNotificationService = genericNotificationService;
            _userManager = userManager;
            _fileService = fileService;
            _notificationService = notificationService;
            _mapper = mapper;
            _urgencyService = urgencyService;
        }

        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.WorkOrder;
         
            var result = _dutyService.GetAll();
            foreach (var duty in result)
            {
                ViewBag.Urgencies = new SelectList(await _urgencyService.GetAllAsync(), "Id", "Description", duty.UrgencyId);
                ViewBag.AppUser = _mapper.Map<AppUserDto>(_userManager.Users.FirstOrDefault(I => I.Id == Convert.ToInt32(duty.AppUserId)));
            }
            return View(_mapper.Map<List<DutyListDto>>(result));
        }

        public IActionResult Detail(int id)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;
            var result = _mapper.Map<DutyListDto>(_dutyService.GetByReportId(id));
            result.AppUser = _mapper.Map<AppUser>(_userManager.Users.FirstOrDefault(I => I.Id == Convert.ToInt32(result.AppUserId)));
            return View(result);
        }

        public IActionResult GetExcel(int id)
        {
            return File(_fileService.ExportExcel(_mapper.Map<List<ReportFileDto>>(_dutyService.GetByReportId(id).Reports)), "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet", Guid.NewGuid() + "xlsx");
        }

        public IActionResult GetPdf(int id)
        {
            var path = _fileService.ExportPdf(_mapper.Map<List<ReportFileDto>>(_dutyService.GetByReportId(id).Reports));
            return File(path, "application/pdf", Guid.NewGuid() + ".pdf");
        }

        public IActionResult AssignDuty(int id, string s, int page = 1)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;
            ViewBag.ActivePage = page;
            ViewBag.Search = s;
            int totalPage;

            var users = _mapper.Map<List<AppUserDto>>(_appUserService.GetNonAdmins(out totalPage, s, page));

            ViewBag.TotalPage = totalPage;
            ViewBag.Users = users;

            return View(_mapper.Map<DutyListDto>(_dutyService.GetByUrgencyId(id)));
        }

        [HttpPost]
        public async Task<IActionResult> AssignDuty(AssignStaffDto model)
        {
            var updatedDuty = await _genericDutyService.FindByIdAsync(model.DutyId);
            updatedDuty.AppUserId = model.StaffId.ToString();
            await _genericDutyService.UpdateAsync(updatedDuty);

            await _genericNotificationService.AddAsync(new Notification
            {
                AppUserId = model.StaffId.ToString(),
                Description = $"{updatedDuty.Name} adlı iş için görevlendirildiniz.",
            });
            return RedirectToAction("Index");
        }

        public IActionResult AssignStaff(AssignStaffDto model)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;

            AssignStaffListDto assignStaffListDto = new AssignStaffListDto();

            assignStaffListDto.AppUser = _mapper.Map<AppUserDto>(_userManager.Users.FirstOrDefault(I => I.Id == model.StaffId));
            assignStaffListDto.Duty = _mapper.Map<DutyListDto>(_dutyService.GetByUrgencyId(model.DutyId));

            return View(assignStaffListDto);
        }
    }
}
