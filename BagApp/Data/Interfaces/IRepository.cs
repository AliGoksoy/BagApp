using BagApp.Data.Entities;
using BagApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BagApp.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        void Remove(T entity);
        Task<T> FindAsync(object id);
        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);

        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC);

        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC);


        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        IQueryable<T> GetQueryable();
    }
}
