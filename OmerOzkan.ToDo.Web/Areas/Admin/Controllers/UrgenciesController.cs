using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.UrgencyDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(RoleInfo.Admin)]
    public class UrgenciesController : Controller
    {
        private readonly IGenericService<Urgency> _urgencyService;
        private readonly IMapper _mapper;
        public UrgenciesController(IGenericService<Urgency> urgencyService, IMapper mapper)
        {
            _mapper = mapper;
            _urgencyService = urgencyService;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Active"] = TempdataInfo.Urgency;
            return View(_mapper.Map<List<UrgencyListDto>>(await _urgencyService.GetAllAsync()));
        }

        public IActionResult Add()
        {
            TempData["Active"] = TempdataInfo.Urgency;
            return View(new UrgencyAddDto());
        }

        [HttpPost]
        public async Task<IActionResult> Add(UrgencyAddDto model)
        {
            if (ModelState.IsValid)
            {
                await _urgencyService.AddAsync(new Urgency()
                {
                    Description = model.Description
                });

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["Active"] = TempdataInfo.Urgency;
            return View(_mapper.Map<UrgencyUpdateDto>(await _urgencyService.FindByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UrgencyUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                await _urgencyService.UpdateAsync(new Urgency
                {
                    Id = model.Id,
                    Description = model.Description
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
