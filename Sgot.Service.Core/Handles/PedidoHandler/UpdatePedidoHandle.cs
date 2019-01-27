using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.PedidoRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.PedidoHandler
{
    public class UpdatePedidoHandle : IRequestHandler<UpdatePedido, EntityResponse>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoService _pedidoService;
        private readonly IOculosService _oculosService;
        private readonly ILenteService _lenteService;

        public UpdatePedidoHandle(IPedidoRepository pedidoRepository, IPedidoService pedidoService,
            IOculosService oculosService, ILenteService lenteService)
        {
            _pedidoService = pedidoService;
            _oculosService = oculosService;
            _lenteService = lenteService;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<EntityResponse> Handle(UpdatePedido request, CancellationToken cancellationToken)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var hasPedido = await _pedidoRepository.GetByIdAsync(request.PedidoId).ConfigureAwait(false);

                    if (hasPedido == null)
                        return await Task.FromResult(new EntityResponse(false, false, false, null, "Pedido inexistente."));

                    if (request.PedidoUpdate.Oculos != null)
                    {
                        foreach (var o in request.PedidoUpdate.Oculos)
                        {                            
                            foreach (var l in o.Lentes)
                            {
                                await _lenteService.Put(l).ConfigureAwait(false);
                            }
                            await _oculosService.Put(o).ConfigureAwait(false);
                        }
                    }
                    var pedidoUpdated = await _pedidoService.Put(request.PedidoUpdate).ConfigureAwait(false);
                    scope.Complete();
                    return await Task.FromResult(new EntityResponse(false, true, false, pedidoUpdated, "Pedido atualizado com sucesso"));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var info = string.Format("Houve um erro ao atualizar o cadastro do pedido.\r\nError: {0}\r\nMessage:{1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
            catch (DbUpdateException ex)
            {
                var info = string.Format("Houve um erro ao atualizar o cadastro do pedido.\r\nError: {0}\r\nMessage:{1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
            catch (Exception ex)
            {
                var info = string.Format("Houve um erro ao atualizar o cadastro do pedido.\r\nError: {0}\r\nMessage:{1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
        }
    }
}