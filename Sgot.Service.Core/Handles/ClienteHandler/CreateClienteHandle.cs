using MediatR;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.ClienteRequest;
using Sgot.Service.Core.Notifications;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles
{
    public class CreateClienteHandle : IRequestHandler<CreateCliente, EntityResponse>
    {
        private readonly IClienteService _clienteService;
        private readonly IMediator _mediator;

        public CreateClienteHandle(IClienteService clienteService, IMediator mediator)
        {
            _clienteService = clienteService;
            _mediator = mediator;
        }

        public async Task<EntityResponse> Handle(CreateCliente request, CancellationToken cancellationToken)
        {
            try
            {
                var clienteCreated = await _clienteService.Post(request.Cliente).ConfigureAwait(false);
                var response = new EntityResponse(true, false, false, clienteCreated, "Pedido criado com sucesso", request);
                await _mediator.Publish(new ClienteCreated(true)).ConfigureAwait(false);
                return await Task.FromResult(response);
            }
            catch(InvalidOperationException ex)
            {
                var info = string.Format("Houve um erro ao cadastrar o cliente.\r\nErro: {0}\r\nMessage: {1}", 
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
            catch (Exception ex)
            {
                var info = string.Format("Houve um erro ao cadastrar o cliente.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
        }
    }
}
