using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Web.BaseControllers
{
    public class BaseIdentityController : Controller
    {
        protected readonly UserManager<AppUser> _userManager;
        public BaseIdentityController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        protected async Task<AppUser> GetLoggedUser()
        {
            return await _userManager.FindByNameAsync(User.Identity.Name);
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
