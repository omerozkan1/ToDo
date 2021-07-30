using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Business.StringInfos;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;
using OmerOzkan.ToDo.Entities.Domains;
using OmerOzkan.ToDo.Web.BaseControllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.Areas.Member.Controllers
{
    [Authorize(Roles = RoleInfo.Member)]
    [Area(RoleInfo.Member)]
    public class DutiesController : BaseIdentityController
    {
        private readonly IDutyService _dutyService;
        private readonly IMapper _mapper;

        public DutiesController(IDutyService dutyService, IMapper mapper, UserManager<AppUser> userManager) : base(userManager)
        {
            _mapper = mapper;
            _dutyService = dutyService;
        }

        public async Task<IActionResult> Index(int activePage = 1)
        {
            TempData["Active"] = TempdataInfo.Duty;
            var user = await GetLoggedUser();

            int totalPage;

            var duties = _mapper.Map<List<DutyListDto>>(_dutyService.GetAllByIncomplete(out totalPage, user.Id.ToString(), activePage));

            ViewBag.TotalPage = totalPage;
            ViewBag.ActivePage = activePage;

            return View(duties);
        }       
    }
}
