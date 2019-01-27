using Microsoft.AspNetCore.Identity;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sgot.Domain.Interfaces.Repositories
{
    public interface IAuthRepository<TEntity> where TEntity : IdentityUser
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(int page, int pageSize);                
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(long id);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(TEntity obj);        
        void Delete(Expression<Func<TEntity, bool>> expression);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        long Update(Expression<Func<TEntity, bool>> expression, TEntity obj);
        Task DeleteAsync(TEntity obj);
        Task DeleteAsync(long id);
        void DeleteAll();
    }
}
