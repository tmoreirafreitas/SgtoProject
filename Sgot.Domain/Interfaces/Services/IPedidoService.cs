using Sgot.Domain.Entities;
using Sgot.Domain.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sgot.Domain.Interfaces.Services
{
    public interface IPedidoService : IService<Pedido>
    {
        Task<Pedido> GetByTso(long tso);
        Task<IEnumerable<Pedido>> GetByEntrega(DateTime entrega);
        Task<IEnumerable<Pedido>> GetBySolicitacao(DateTime solicitacao);
        Task<IEnumerable<Pedido>> GetByMedico(string nome);
        Task<IEnumerable<Pedido>> GetByFormaPagamento(FormaPagamento formaPagamento);
        Task<IEnumerable<Pedido>> GetByCpfCliente(string cpf);
        Task<IEnumerable<Pedido>> GetByRgCliente(string rg);
        Task<IEnumerable<Pedido>> GetByTelCliente(string tel);
    }
}
