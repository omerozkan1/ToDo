using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;
using OmerOzkan.ToDo.Dto.Dtos.ReportDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    public class WorkOrderController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IDutyService _dutyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public WorkOrderController(IAppUserService appUserService, IDutyService dutyService, UserManager<AppUser> userManager, IFileService fileService, INotificationService notificationService, IMapper mapper)
        {
            _appUserService = appUserService;
            _dutyService = dutyService;
            _userManager = userManager;
            _fileService = fileService;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.WorkOrder;
            return View(_mapper.Map<List<DutyListDto>>(_dutyService.GetAll()));
        }

        public IActionResult Detail(int id)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;
            return View(_mapper.Map<DutyListDto>(_dutyService.GetByReportId(id)));
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

        public IActionResult AtaPersonel(int id, string s, int sayfa = 1)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;
            ViewBag.AktifSayfa = sayfa;
            ViewBag.Aranan = s;
            int toplamSayfa;

            var personeller = _mapper.Map<List<AppUserDto>>(_appUserService.GetNonAdmins(out toplamSayfa, s, sayfa));

            ViewBag.ToplamSayfa = toplamSayfa;
            ViewBag.Personeller = personeller;

            return View(_mapper.Map<DutyListDto>(_dutyService.GetByUrgencyId(id)));
        }

        [HttpPost]
        public IActionResult AssignStaff(AssignStaffDto model)
        {
            var updatedDuty = _dutyService.FindByIdAsync(model.DutyId);
            updatedDuty.Result.AppUserId = model.StaffId.ToString();
            _dutyService.UpdateAsync(updatedDuty.Result);

            _notificationService.AddAsync(new Notification
            {
                AppUserId = model.StaffId.ToString(),
                Description = $"{updatedDuty.Result.Name} adlı iş için görevlendirildiniz."
            });

            return RedirectToAction("Index");
        }

        public IActionResult GorevlendirPersonel(AssignStaffDto model)
        {
            TempData["Active"] = TempdataInfo.WorkOrder;

            AssignStaffListDto assignStaffListDto = new AssignStaffListDto();

            assignStaffListDto.AppUser = _mapper.Map<AppUserDto>(_userManager.Users.FirstOrDefault(I => I.Id == model.StaffId));
            assignStaffListDto.Duty = _mapper.Map<DutyListDto>(_dutyService.GetByUrgencyId(model.DutyId));

            return View(assignStaffListDto);
        }
    }
}
