using OmerOzkan.ToDo.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.DataAccess.UnitofWork
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        Task SaveChanges();
    }
}
