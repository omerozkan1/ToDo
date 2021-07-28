using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> CheckUserAsync(AppUserLoginDto appUserLoginDto);
        Task<AppUser> FindByNameAsync(string userName);
        List<AppUser> GetNonAdmins();
        List<AppUser> GetNonAdmins(out int totalPage, string searchKey, int activePage = 1);
        List<AppUserDutyInfo> GetMostCompleteDutyUsers();
        List<AppUserDutyInfo> GetMostEmployedUsers();
    }
}
