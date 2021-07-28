using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> CheckUserAsync(AppUserLoginDto appUserLoginDto);
        Task<AppUser> FindByNameAsync(string userName);
        bool SignUpAsync(AppUser user);
    }
}
