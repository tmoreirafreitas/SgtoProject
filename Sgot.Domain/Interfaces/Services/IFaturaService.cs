using Sgot.Domain.Entities;
using Sgot.Domain.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sgot.Domain.Interfaces.Services
{
    public interface IFaturaService : IService<Fatura>
    {
        Task<Fatura> GetFaturaByTso(long tso);
        Task<IEnumerable<Fatura>> GetFaturaByFormaPagamento(FormaPagamento formaPagamento);
        Task<IEnumerable<Fatura>> GetFaturaByStatus(bool isPaga);
    }
}
