using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.UrgencyDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleInfo.Admin)]
    [Area(RoleInfo.Admin)]
    public class UrgenciesController : Controller
    {
        private readonly IUrgencyService _urgencyService;
        private readonly IMapper _mapper;
        public UrgenciesController(IUrgencyService urgencyService, IMapper mapper)
        {
            _mapper = mapper;
            _urgencyService = urgencyService;
        }
        public IActionResult Index()
        {
            TempData["Active"] = TempdataInfo.Urgency;

            return View(_mapper.Map<List<UrgencyListDto>>(_urgencyService.GetAllAsync()));
        }

        public IActionResult Add()
        {
            TempData["Active"] = TempdataInfo.Urgency;
            return View(new UrgencyAddDto());
        }

        [HttpPost]
        public IActionResult Add(UrgencyAddDto model)
        {
            if (ModelState.IsValid)
            {
                _urgencyService.AddAsync(new Urgency()
                {
                    Description = model.Description
                });

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Update(int id)
        {
            TempData["Active"] = TempdataInfo.Urgency;
            return View(_mapper.Map<UrgencyUpdateDto>(_urgencyService.FindByIdAsync(id)));
        }

        [HttpPost]
        public IActionResult Update(UrgencyUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                _urgencyService.UpdateAsync(new Urgency
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
