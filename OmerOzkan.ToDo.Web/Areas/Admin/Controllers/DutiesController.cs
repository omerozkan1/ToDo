using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(RoleInfo.Admin)]
    public class DutiesController : Controller
    {
        private readonly IDutyService _dutyService;
        private readonly IGenericService<Urgency> _genericUrgencyService;
        private readonly IGenericService<Duty> _genericDutyService;
        private readonly IUrgencyService _urgencyService;
        private readonly IMapper _mapper;

        public DutiesController(IDutyService dutyService, IGenericService<Urgency> genericUrgencyService, IGenericService<Duty> genericDutyService, IUrgencyService urgencyService, IMapper mapper)
        {
            _mapper = mapper;
            _dutyService = dutyService;
            _genericUrgencyService = genericUrgencyService;
            _genericDutyService = genericDutyService;
            _urgencyService = urgencyService;
        }

        public IActionResult Index()
        {
            return View(_mapper.Map<List<DutyListDto>>(_dutyService.GetByIncompleteWithUrgency()));
        }

        public async Task<IActionResult> Add()
        {
            TempData["Active"] = TempdataInfo.Duty;
            ViewBag.Urgencies = new SelectList(await _genericUrgencyService.GetAllAsync(), "Id", "Description");
            return View(new DutyAddDto());
        }

        [HttpPost]
        public async Task<IActionResult> Add(DutyAddDto model)
        {
            if (ModelState.IsValid)
            {
                await _genericDutyService.AddAsync(new Duty
                {
                    Description = model.Description,
                    Name = model.Name,
                    UrgencyId = model.UrgencyId
                });

                return RedirectToAction("Index");
            }
            ViewBag.Urgencies = new SelectList(await _genericUrgencyService.GetAllAsync(), "Id", "Description");
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["Active"] = TempdataInfo.Duty;
            var duty = await _genericDutyService.FindByIdAsync(id);
            ViewBag.Urgencies = new SelectList(await _genericUrgencyService.GetAllAsync(), "Id", "Description", duty.UrgencyId);
            return View(_mapper.Map<DutyUpdateDto>(duty));
        }

        [HttpPost]
        public async Task<IActionResult> Update(DutyUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                await _genericDutyService.UpdateAsync(new Duty()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    UrgencyId = model.UrgencyId
                });

                return RedirectToAction("Index");
            }
            ViewBag.Urgencies = new SelectList(await _genericUrgencyService.GetAllAsync(), "Id", "Description", model.UrgencyId);
            return View(model);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _genericDutyService.RemoveAsync(new Duty { Id = id });
            return Json(null);
        }
    }
}
