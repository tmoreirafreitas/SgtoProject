using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sgot.Service
{
    public class FaturaService : Service<Fatura>, IFaturaService
    {
        private readonly IFaturaRepository _repository;
        private readonly IPedidoService _pedidoService;
        private readonly IUnitOfWork _uow;

        public FaturaService(IFaturaRepository repository, IPedidoService pedidoService, IUnitOfWork uow) : base(repository, uow)
        {
            _repository = repository;
            _pedidoService = pedidoService;
            _uow = uow;
        }

        public async Task<IEnumerable<Fatura>> GetFaturaByFormaPagamento(FormaPagamento formaPagamento)
        {
            return await Task.Run(() =>
            {
                return _pedidoService.GetAsync(p => p.Fatura.FormaPagamento.Equals(formaPagamento)).Result.Select(p => p.Fatura);
            });
        }

        public async Task<IEnumerable<Fatura>> GetFaturaByStatus(bool isPaga)
        {
            return await Task.Run(() =>
            {
                return _pedidoService.GetAsync(p => p.Fatura.IsPaga.Equals(isPaga)).Result.Select(p => p.Fatura);
            });
        }

        public async Task<Fatura> GetFaturaByTso(long tso)
        {
            var pedido = await _pedidoService.GetByTso(tso);
            Fatura fatura = null;
            if (pedido != null)
                fatura = pedido.Fatura;

            return fatura;
        }

        public override Task<Fatura> Put(Fatura obj)
        {
            return Task.Run(() =>
            {
                var pedido = _pedidoService.GetAsync(p => p.Fatura.Id.Equals(obj.Id)).Result.FirstOrDefault();
                pedido.Fatura = obj;
                _pedidoService.Put(p => p.Id.Equals(pedido.Id), pedido);
                return pedido.Fatura;
            });
        }
    }
}
