using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>,IQueryable<T>> include = null
            ,bool AsNoTracking = false);

        Task<T> GetByConditionAsync(
            Expression<Func<T, bool>> prediecate
            , Func<IQueryable<T>, IQueryable<T>> include=null,
             bool AsNoTracking = false);
        Task<IEnumerable<T>> FindAsync(
              Expression<Func<T, bool>> predicate,
              Func<IQueryable<T>, IQueryable<T>> include = null,
              bool AsNoTracking = false);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<int> SaveChange();

    }
}
