using System.Collections.Generic;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.Business.Interfaces
{
    public interface IGenericService<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
