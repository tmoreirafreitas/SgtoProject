using FluentValidation;
using Sgot.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sgot.Domain.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : Entity
    {
        Task<TEntity> Get(long id);
        Task<IQueryable<TEntity>> Get();
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IQueryable<TEntity>> Get(int page, int pageSize);
        Task<TEntity> Post(TEntity obj);
        Task<TEntity> Put(TEntity obj);
        long Put(Expression<Func<TEntity, bool>> predicate, TEntity obj);
        Task Delete(long id);
    }
}
