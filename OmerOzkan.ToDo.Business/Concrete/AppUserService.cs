using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class AppUserService : GenericService<AppUser>, IAppUserService
    {
        private readonly IGenericDal<AppUser> _genericDal;
        private readonly IAppUserDal _appUserDal;
        public AppUserService(IGenericDal<AppUser> genericDal, IAppUserDal appUserDal) : base(genericDal)
        {
            _genericDal = genericDal;
            _appUserDal = appUserDal;
        }
        public List<AppUser> GetNonAdmins()
        {
            return _appUserDal.GetNonAdmins();
        }

        public List<AppUser> GetNonAdmins(out int totalPage, string searchKey, int activePage)
        {
            return _appUserDal.GetNonAdmins(out totalPage, searchKey, activePage);
        }

        public List<AppUserDutyInfo> GetMostEmployedUsers()
        {
            return _appUserDal.GetMostEmployedUsers();
        }

        public List<AppUserDutyInfo> GetMostCompleteDutyUsers()
        {
            return _appUserDal.GetMostCompleteDutyUsers();
        }

        public async Task<AppUser> CheckUserAsync(AppUserLoginDto appUserLoginDto)
        {
            return await _genericDal.GetAsync(I => I.Email == appUserLoginDto.Email /*&& I.Password == appUserLoginDto.Password*/);
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _genericDal.GetAsync(I => I.UserName == userName);
        }
    }
}
