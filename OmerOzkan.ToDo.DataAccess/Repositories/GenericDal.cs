using Microsoft.EntityFrameworkCore;
using OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context;
using OmerOzkan.ToDo.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OmerOzkan.ToDo.DataAccess.Repositories
{
    public class GenericDal<T> : IGenericDal<T> where T : class, new()
    {
        private readonly ToDoContext _context;
        public GenericDal(ToDoContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> keySelector)
        {
            return await _context.Set<T>().Where(filter).OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            return await _context.Set<T>().OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
