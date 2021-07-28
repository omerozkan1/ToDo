using OmerOzkan.ToDo.Business.Interfaces;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Entities.Domains;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Concrete
{
    public class AppUserService : GenericService<AppUser>, IAppUserService
    {
        private readonly IGenericDal<AppUser> _genericDal;
        public AppUserService(IGenericDal<AppUser> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppUser> CheckUserAsync(AppUserLoginDto appUserLoginDto)
        {
            return await _genericDal.GetAsync(I => I.UserName == appUserLoginDto.UserName /*&& I.Password == appUserLoginDto.Password*/);
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await _genericDal.GetAsync(I => I.UserName == userName);
        }

        public bool SignUpAsync(AppUser user)
        {
            var isAnyUser = _genericDal.GetAsync(I => I.UserName == user.UserName);
            if (isAnyUser == null)
            {
                var result = _genericDal.AddAsync(new AppUser
                {
                    Name = user.Name,
                    SurName = user.SurName,
                    Email = user.Email,
                    //Password = user.Password,
                    UserName = user.UserName             
                });

                if (result.IsCompletedSuccessfully)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
