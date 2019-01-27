using Sgot.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sgot.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(int page, int pageSize);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllAsync(int page, int pageSize);
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(long id);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task<TEntity> AddAsync(TEntity obj);
        Task DeleteAsync(Expression<Func<TEntity, bool>> expression);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        long Update(Expression<Func<TEntity, bool>> expression, TEntity obj);
        Task DeleteAsync(TEntity obj);
        Task DeleteAsync(long id);
        void DeleteAll();
    }
}
