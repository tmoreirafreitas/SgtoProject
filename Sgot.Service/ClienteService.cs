using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Validators;
using System.Linq;
using System;
using System.Threading.Tasks;
using Sgot.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Sgot.Service
{
    public class ClienteService : Service<Cliente>, IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IUnitOfWork _uow;
        public ClienteService(IClienteRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            _repository = repository;
            _uow = unitOfWork;
        }

        public async Task<Cliente> GetByCpf(string cpf)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(cpf))
                {
                    throw new ArgumentException("O CPF não pode ser vazia ou nulo.");
                }
                return _repository.SingleAsync(c => c.Cpf.Equals(cpf));
            });
        }

        public async Task<Cliente> GetByEmail(string email)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentException("O E-mail não pode ser vazia ou nulo.");
                }
                return _repository.SingleAsync(c => c.Email.Equals(email));
            });
        }

        public async Task<Cliente> GetByRg(string rg)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(rg))
                {
                    throw new ArgumentException("O RG não pode ser vazia ou nulo.");
                }
                return _repository.SingleAsync(c => c.Cpf.Equals(rg));
            });
        }

        public async Task<IEnumerable<Cliente>> GetBySpc(bool status)
        {
            return await Task.Run(() =>
            {
                return _repository.GetAsync(c => c.IsSPC.Equals(status));
            });
        }

        public async Task<Cliente> GetByTel(string telefone)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(telefone))
                {
                    throw new ArgumentException("O Telefone não pode ser vazia ou nulo.");
                }
                return _repository.SingleAsync(c => c.Cpf.Equals(telefone));
            });
        }
    }
}
