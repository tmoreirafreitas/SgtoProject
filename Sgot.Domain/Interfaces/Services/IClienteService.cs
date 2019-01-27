using Sgot.Domain.Entities;
using Sgot.Domain.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sgot.Domain.Interfaces.Services
{
    public interface IClienteService : IService<Cliente>
    {
        Task<IEnumerable<Cliente>> GetBySpc(bool status);
        Task<Cliente> GetByRg(string rg);
        Task<Cliente> GetByCpf(string cpf);
        Task<Cliente> GetByTel(string tel);
        Task<Cliente> GetByEmail(string email);
    }
}
