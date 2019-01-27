using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.ClienteRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.ClienteHandler
{
    public class UpdateClienteCommandHandle : IRequestHandler<UpdateCliente, EntityResponse>
    {
        private readonly IClienteService _clienteService;
        private readonly IClienteRepository _clienteRepository;        

        public UpdateClienteCommandHandle(IClienteRepository clienteRepository, IClienteService clienteService)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;            
        }

        public async Task<EntityResponse> Handle(UpdateCliente request, CancellationToken cancellationToken)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var hasCliente = await _clienteRepository.GetByIdAsync(request.Id);
                    if(hasCliente == null)
                        return await Task.FromResult(new EntityResponse(false, false, false, null, "Cliente inexistente.", request));
                    
                    var clienteUpdated = await _clienteService.Put(request.Cliente).ConfigureAwait(false);
                    scope.Complete();
                    var info = "O Cadastro do cliente foi atualizado com sucesso";
                    return await Task.FromResult(new EntityResponse(false, true, false, request.Cliente, info, request));
                }                
                catch(DbUpdateConcurrencyException ex)
                {
                    var info = string.Format("Houve um erro ao atualizar o cadastro do cliente.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (DbUpdateException ex)
                {
                    var info = string.Format("Houve um erro ao atualizar o cadastro do cliente.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (Exception ex)
                {
                    var info = string.Format("Houve um erro ao atualizar o cadastro do cliente.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
            }
        }
    }
}
