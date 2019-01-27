using MediatR;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.PedidoRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.PedidoHandler
{
    public class DeletePedidoHandle : IRequestHandler<DeletePedido, EntityResponse>
    {
        private readonly IPedidoService _pedidoService;
        private readonly IOculosService _oculosService;
        private readonly ILenteService _lenteService;

        public DeletePedidoHandle(IPedidoService pedidoService, IOculosService oculosService, ILenteService lenteService)
        {
            _pedidoService = pedidoService;
            _oculosService = oculosService;
            _lenteService = lenteService;
        }

        public async Task<EntityResponse> Handle(DeletePedido request, CancellationToken cancellationToken)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var hasPedido = await _pedidoService.Get(request.Id).ConfigureAwait(false);
                    if (hasPedido == null)
                        return await Task.FromResult(new EntityResponse(false, false, false, null, "Pedido inexistente."));

                    var oculos = await _oculosService.GetAsync(o => o.PedidoId.Equals(hasPedido.Id))
                        .ConfigureAwait(false);
                    foreach (var o in oculos)
                    {
                        hasPedido.Oculos.Add(o);
                    }

                    foreach (var ocls in oculos)
                    {
                        var lentes = await _lenteService.GetAsync(l => l.OculosId.Equals(ocls.Id))
                            .ConfigureAwait(false);
                        if (lentes != null)
                        {
                            await _lenteService.Delete(lentes.ToList()[0].Id).ConfigureAwait(false);
                            await _lenteService.Delete(lentes.ToList()[1].Id).ConfigureAwait(false);
                        }
                        await _oculosService.Delete(ocls.Id).ConfigureAwait(false);
                    }

                    await _pedidoService.Delete(hasPedido.Id).ConfigureAwait(false);
                    scope.Complete();
                    return await Task.FromResult(new EntityResponse(false, false, true, hasPedido, "Pedido foi deletado com sucesso."));
                }
            }
            catch (InvalidOperationException ex)
            {
                var info = string.Format("Houve um erro ao deletar o pedido.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
            catch (Exception ex)
            {
                var info = string.Format("Houve um erro ao deletar o pedido.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
        }
    }
}
