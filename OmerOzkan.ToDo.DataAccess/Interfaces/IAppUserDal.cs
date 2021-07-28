using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.DataAccess.Interfaces
{
    public interface IAppUserDal : IGenericDal<AppUser>
    {
        List<AppUser> GetNonAdmins();
        List<AppUser> GetNonAdmins(out int totalPage, string searchKey, int activePage = 1);
        List<AppUserDutyInfo> GetMostCompleteDutyUsers();
        List<AppUserDutyInfo> GetMostEmployedUsers();
    }
}
