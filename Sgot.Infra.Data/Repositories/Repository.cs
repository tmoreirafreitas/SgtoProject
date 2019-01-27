using Microsoft.EntityFrameworkCore;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sgot.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity obj)
        {
            var itemResult = await _dbSet.AddAsync(obj);
            return itemResult.Entity;
        }

        public void DeleteAll()
        {
            _dbSet.RemoveRange(_dbSet);
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity obj)
        {
            return Task.Run(() =>
            {
                var result = _dbSet.Find(obj.Id);
                if (result != null)
                    _dbSet.Remove(result);
            });
        }

        public Task DeleteAsync(long id)
        {
            return Task.Run(() =>
            {
                var result = _dbSet.Find(id);
                if (result != null)
                    _dbSet.Remove(result);
            });
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Any(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetAll(int page, int pageSize)
        {
            return _dbSet.Skip(page).Take(pageSize);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return _dbSet.AsQueryable();
            });
        }

        public Task<IQueryable<TEntity>> GetAllAsync(int page, int pageSize)
        {
            return Task.Run(() =>
            {
                return _dbSet.Skip(page).Take(pageSize);
            });
        }

        public Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Task.Run(() =>
            {
                return _dbSet.Where(expression);
            });
        }

        public Task<TEntity> GetByIdAsync(long id)
        {
            return Task.Run(() =>
            {
                return _dbSet.SingleAsync(o => o.Id.Equals(id));
            });
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Task.Run(() =>
            {
                return _dbSet.SingleAsync(expression);
            });
        }

        public long Update(Expression<Func<TEntity, bool>> expression, TEntity obj)
        {
            var updated = _dbSet.Update(obj);
            updated.State = EntityState.Modified;
            return updated.Entity.Id;
        }

        public Task<TEntity> UpdateAsync(TEntity obj)
        {
            return Task.Run(() =>
            {
                var updated = _dbSet.Update(obj);
                updated.State = EntityState.Modified;
                return updated.Entity;
            });
        }
    }
}