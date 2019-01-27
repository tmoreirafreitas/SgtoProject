using MediatR;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.FaturaRequest;
using Sgot.Service.Core.Commands.ParcelaRequest;
using Sgot.Service.Core.Commands.PedidoRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.FaturaHandler
{
    public class DeleteFaturaHandle : IRequestHandler<DeleteFatura, EntityResponse>
    {
        private readonly IMediator _mediator;
        private readonly IFaturaService _faturaService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IParcelaRepository _parcelaRepository;

        public DeleteFaturaHandle(IMediator mediator, IFaturaService faturaService,
            IPedidoRepository pedidoRepository, IParcelaRepository parcelaRepository)
        {
            _mediator = mediator;
            _faturaService = faturaService;
            _pedidoRepository = pedidoRepository;
            _parcelaRepository = parcelaRepository;
        }

        public async Task<EntityResponse> Handle(DeleteFatura request, CancellationToken cancellationToken)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var hasFatura = await _faturaService.Get(request.Id).ConfigureAwait(false);
                    if (hasFatura == null)
                    {
                        var info = "Não existe fatura na base de dados com o Id especificado";
                        return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                    }

                    var pedido = await _pedidoRepository.GetByIdAsync(hasFatura.PedidoId).ConfigureAwait(false);
                    var parcelas = await _parcelaRepository.GetAsync(p => p.FaturaId.Equals(hasFatura.Id)).ConfigureAwait(false);

                    //Deleta todas as parcelas
                    foreach (var p in parcelas)
                    {
                        hasFatura.Parcelas.Add(p);
                        await _mediator.Send(new DeleteParcela(p.Id)).ConfigureAwait(false);
                    }

                    //Deleta o pedido
                    if (pedido != null)
                    {
                        hasFatura.Pedido = pedido;
                        await _mediator.Send(new DeletePedido(pedido.Id)).ConfigureAwait(false);
                    }

                    //Deleta a fatura
                    await _faturaService.Delete(hasFatura.Id).ConfigureAwait(false);
                    scope.Complete();

                    var msg = string.Format("A fatura {0} foi deletada com sucesso", hasFatura.Id);
                    return await Task.FromResult(new EntityResponse(false, false, true, null, msg, request));
                }
                catch (InvalidOperationException ex)
                {
                    var info = string.Format("Houve um erro ao deletar a fatura.\r\nErro: {0}\r\nMessage: {1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (Exception ex)
                {
                    var info = string.Format("Houve um erro ao deletar a fatura.\r\nErro: {0}\r\nMessage: {1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
            }
        }
    }
}
