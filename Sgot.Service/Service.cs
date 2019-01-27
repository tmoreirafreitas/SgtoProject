using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Services;
using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sgot.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Sgot.Service
{
    public class Service<TEntity> : IService<TEntity> where TEntity : Entity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _uow;        

        public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _uow = unitOfWork;
        }

        public async Task Delete(long id)
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    throw new ArgumentException("O id não pode ser nulo ou vazio.");

                _repository.DeleteAsync(id);
                _uow.Commit();
            });
        }

        public async Task<TEntity> Get(long id)
        {
            return await Task.Run(() =>
             {
                 if (string.IsNullOrEmpty(id.ToString()))
                     throw new ArgumentException("O id não pode ser nulo ou vazio.");

                 return _repository.GetByIdAsync(id);
             });
        }

        public Task<IQueryable<TEntity>> Get()
        {
            return Task.Run(() =>
            {
                return _repository.GetAll();
            });
        }

        public Task<IQueryable<TEntity>> Get(int page, int pageSize)
        {
            if (page == 0)
                throw new ArgumentException("O page não pode ser zero.");

            if (pageSize == 0)
                throw new ArgumentException("O pageSize não pode ser zero.");

            return Task.Run(() =>
            {
                return _repository.GetAll(page, pageSize);
            });
        }

        public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            var objs = await _repository.GetAsync(expression);
            return objs;
        }

        public virtual async Task<TEntity> Post(TEntity obj)
        {            
            var item = await _repository.AddAsync(obj);
            _uow.Commit();
            return item;
        }

        public virtual async Task<TEntity> Put(TEntity obj)
        {            
            return await Task.Run(() =>
            {
                var objUpdated = _repository.UpdateAsync(obj);
                _uow.Commit();
                return objUpdated;
            });
        }

        public virtual long Put(Expression<Func<TEntity, bool>> predicate, TEntity obj)
        {
            var objUpdated = _repository.Update(predicate, obj);
            _uow.Commit();
            return objUpdated;
        }
    }
}