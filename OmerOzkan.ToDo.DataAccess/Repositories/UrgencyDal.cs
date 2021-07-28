using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class UrgencyDal : GenericDal<Urgency>, IUrgencyDal
    {
        public UrgencyDal(ToDoContext context) : base(context)
        {
        }
    }
}
