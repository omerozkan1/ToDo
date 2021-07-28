using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class DutyDal : GenericDal<Duty>, IDutyDal
    {
        public DutyDal(ToDoContext context) : base(context)
        {
        }
    }
}
