using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class AppUserDal : GenericDal<AppUser>, IAppUserDal
    {
        public AppUserDal(ToDoContext context) : base(context)
        {
        }
    }
}
