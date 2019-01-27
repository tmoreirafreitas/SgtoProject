using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Sgot.Service
{
    public class PedidoService : Service<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IUnitOfWork _uow;

        public PedidoService(IPedidoRepository repository, IUnitOfWork uow) : base(repository, uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<IEnumerable<Pedido>> GetByCpfCliente(string cpf)
        {
            var pedidos = await _repository.GetAsync(p => p.Cliente.Cpf.Equals(cpf));
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByEntrega(DateTime entrega)
        {
            var pedidos = await _repository.GetAsync(
                p => p.DataEntrega.Day.Equals(entrega.Day) &&
                p.DataEntrega.Month.Equals(entrega.Month) &&
                p.DataEntrega.Year.Equals(entrega.Year));
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByFormaPagamento(FormaPagamento formaPagamento)
        {
            var pedidos = await _repository.GetAsync(
                    p => p.FormaPagamento.ToString().ToLowerInvariant() == formaPagamento.ToString().ToLowerInvariant()
                );
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByMedico(string nome)
        {
            var pedidos = await _repository.GetAsync(p => p.Medico.Contains(nome));
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByRgCliente(string rg)
        {
            var pedidos = await _repository.GetAsync(p => p.Cliente.Rg.Equals(rg));
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetBySolicitacao(DateTime solicitacao)
        {
            var pedidos = await _repository.GetAsync(
                p => p.DataSolicitacao.Day.Equals(solicitacao.Day) &&
                p.DataSolicitacao.Month.Equals(solicitacao.Month) &&
                p.DataSolicitacao.Year.Equals(solicitacao.Year));
            return pedidos;
        }

        public async Task<IEnumerable<Pedido>> GetByTelCliente(string tel)
        {
            var pedidos = await _repository.GetAsync(p => p.Cliente.Telefone.Equals(tel));
            return pedidos;
        }

        public async Task<Pedido> GetByTso(long tso)
        {
            var result = await _repository.GetAsync(p => p.Id.Equals(tso));
            Pedido pedido = null;
            if (result != null)
                pedido = result.FirstOrDefault();

            return pedido;
        }
    }
}
