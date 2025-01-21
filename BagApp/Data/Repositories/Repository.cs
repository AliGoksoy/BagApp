using BagApp.Data.Entities;
using BagApp.Data.Enums;
using BagApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BagApp.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BagContext _context;

        public Repository(BagContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC ? await _context.Set<T>().AsNoTracking().OrderBy(selector).ToListAsync() : await _context.Set<T>().AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC ? await _context.Set<T>().Where(filter).AsNoTracking().OrderBy(selector).ToListAsync() : await _context.Set<T>().Where(filter).AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            var updateEntity = _context.Set<T>().Find(entity.Id);
            _context.Entry(updateEntity).CurrentValues.SetValues(entity);
        }

    }
}
