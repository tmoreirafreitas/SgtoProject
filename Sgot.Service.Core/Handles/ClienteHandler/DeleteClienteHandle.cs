using MediatR;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.ClienteRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.ClienteHandler
{
    public class DeleteClienteHandle : IRequestHandler<DeleteCliente, EntityResponse>
    {
        private readonly IClienteService _clienteService;
        private readonly IClienteRepository _clienteRepository;

        public DeleteClienteHandle(IClienteService clienteService, IClienteRepository clienteRepository)
        {
            _clienteService = clienteService;
            _clienteRepository = clienteRepository;
        }

        public async Task<EntityResponse> Handle(DeleteCliente request, CancellationToken cancellationToken)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var cliente = await _clienteRepository.GetByIdAsync(request.Id);
                    if (cliente != null)
                    {
                        await _clienteService.Delete(request.Id).ConfigureAwait(false);
                        scope.Complete();
                        var msg = string.Format("O {0} cujo CPF: {1} foi deletado com sucesso", cliente.Nome, cliente.Cpf);
                        return await Task.FromResult(new EntityResponse(false, false, true, null, msg, request));
                    }
                    else
                    {
                        var info = "Não existe cliente na base de dados com o Id especificado";
                        return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                    }
                }
            }
            catch(InvalidOperationException ex)
            {
                var info = string.Format("Houve um erro ao deletar o cliente.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
            catch (Exception ex)
            {
                var info = string.Format("Houve um erro ao deletar o cliente.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
        }
    }
}
