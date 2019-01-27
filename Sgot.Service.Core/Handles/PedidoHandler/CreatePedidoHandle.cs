using MediatR;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.PedidoRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles.PedidoHandler
{
    public class CreatePedidoHandle : IRequestHandler<CreatePedido, EntityResponse>
    {
        private readonly IPedidoService _pedidoService;

        public CreatePedidoHandle(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<EntityResponse> Handle(CreatePedido request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Pedido != null)
                {
                    var pedidoCreated = await _pedidoService.Post(request.Pedido).ConfigureAwait(false);
                    var response = new EntityResponse(true, false, false, pedidoCreated, "Pedido criado com sucesso", request);
                    return await Task.FromResult(response);
                }
                return await Task.FromResult(new EntityResponse(false, false, false, null, "Houve um erro ao criar o pedido", request));
            }
            catch (InvalidOperationException ex)
            {
                var info = string.Format("Houve um erro ao cadastrar o pedido.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
            catch (Exception ex)
            {
                var info = string.Format("Houve um erro ao cadastrar o pedido.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
        }
    }
}
