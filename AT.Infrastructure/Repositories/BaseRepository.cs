using AT.Domain.Entities;
using AT.Domain.Interfaces;
using AT.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AT.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T: BaseEntity
    {
        public readonly ATDbContext _dbContext;
        public BaseRepository(ATDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> AddAsync(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
        public async Task UpdateAsync(T item)
        {
            _dbContext.Set<T>().Entry(item).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task Remove(T item)
        {
            _dbContext.Set<T>().Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
