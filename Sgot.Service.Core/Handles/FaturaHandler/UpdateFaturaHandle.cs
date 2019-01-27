using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.ClienteRequest;
using Sgot.Service.Core.Commands.FaturaRequest;
using Sgot.Service.Core.Commands.PedidoRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.FaturaHandler
{
    public class UpdateFaturaHandle : IRequestHandler<UpdateFatura, EntityResponse>
    {
        private readonly IFaturaService _faturaService;
        private readonly IMediator _mediator;

        public UpdateFaturaHandle(IFaturaService faturaService, IMediator mediator)
        {
            _faturaService = faturaService;
            _mediator = mediator;
        }

        public async Task<EntityResponse> Handle(UpdateFatura request, CancellationToken cancellationToken)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var hasFatura = await _faturaService.Get(request.Id).ConfigureAwait(false);

                    if (hasFatura == null)
                        return await Task.FromResult(new EntityResponse(false, false, false, null, "Fatura inexistente.", request));

                    if (request.Fatura.Pedido != null)
                        await _mediator.Send(new UpdatePedido(request.Fatura.PedidoId, request.Fatura.Pedido)).ConfigureAwait(false);

                    if (request.Fatura.Cliente != null)
                        await _mediator.Send(new UpdateCliente(request.Fatura.ClienteId, request.Fatura.Cliente)).ConfigureAwait(false);

                    var faturaUpdated = await _faturaService.Put(request.Fatura).ConfigureAwait(false);
                    scope.Complete();

                    var info = "O Cadastro da fatura do cliente foi atualizado com sucesso";
                    return await Task.FromResult(new EntityResponse(false, true, false, request.Fatura, info, request));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var info = string.Format("Houve um erro ao atualizar a fatura do cliente.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (DbUpdateException ex)
                {
                    var info = string.Format("Houve um erro ao atualizar a fatura do cliente.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (Exception ex)
                {
                    var info = string.Format("Houve um erro ao atualizar a fatura do cliente.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
            }
        }
    }
}
