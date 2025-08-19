using Microsoft.EntityFrameworkCore;
using SmartChat.Domain.Interface;
using SmartChat.Infrastructre.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Repository
{
    public class BaseSQLRepository<T> : IRepository<T> where T : class
    {
        private readonly SmartChatDbContext _smartChatDbContext;

        public BaseSQLRepository(SmartChatDbContext smartChatDbContext)
        {
            _smartChatDbContext = smartChatDbContext;
        }

        public virtual async Task AddAsync(T entity)
        {
          await _smartChatDbContext.Set<T>().AddAsync(entity);    
        }

        public virtual void Delete(T entity)
        {
             _smartChatDbContext.Set<T>().Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> FindAsync
            (Expression<Func<T, bool>> predicate
            , Func<IQueryable<T>, IQueryable<T>> include= null
            , bool AsNoTracking = false)
        {
            IQueryable<T> query = _smartChatDbContext.Set<T>();

            if(AsNoTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>> include = null
            , bool AsNoTracking = false)
        {
           var query = _smartChatDbContext.Set<T>().AsQueryable();

            if (AsNoTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByConditionAsync(
            Expression<Func<T, bool>> prediecate,
            Func<IQueryable<T>,
                IQueryable<T>> include = null,
            bool AsNoTracking = false)
        {
            var query = _smartChatDbContext.Set<T>().AsQueryable();

            if (AsNoTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(prediecate);
        }

        public virtual async Task<int> SaveChange()
        {
           return await _smartChatDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _smartChatDbContext.Set<T>().Update(entity);
        }
    }
}
