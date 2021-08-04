using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.BaseControllers
{
    public class BaseIdentityController : Controller
    {
        protected readonly IAppUserService _appUserService;
        public BaseIdentityController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        protected async Task<AppUser> GetLoggedUser()
        {
            return await _appUserService.FindByNameAsync(User.Identity.Name);
        }

        protected void AddError(IEnumerable<IdentityError> errors)
        {
            foreach (var item in errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }
    }
}
