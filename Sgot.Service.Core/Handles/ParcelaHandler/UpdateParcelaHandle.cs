using MediatR;
using Microsoft.EntityFrameworkCore;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.ParcelaRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.ParcelaHandler
{
    public class UpdateParcelaHandle : IRequestHandler<UpdateParcela, EntityResponse>
    {
        private readonly IParcelaService _parcelaService;
        private readonly IParcelaRepository _parcelaRepository;

        public UpdateParcelaHandle(IParcelaRepository parcelaRepository, IParcelaService parcelaService)
        {
            _parcelaRepository = parcelaRepository;
            _parcelaService = parcelaService;
        }

        public async Task<EntityResponse> Handle(UpdateParcela request, CancellationToken cancellationToken)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var hasParcela = await _parcelaRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
                    if (hasParcela == null)
                        return await Task.FromResult(new EntityResponse(false, false, false, null, "Parcela inexistente.", request));
                    var parcelaUpdated = await _parcelaService.Put(request.Parcela).ConfigureAwait(false);
                    scope.Complete();
                    var info = "O Cadastro da parcela foi atualizado com sucesso";
                    return await Task.FromResult(new EntityResponse(false, true, false, parcelaUpdated, info, request));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var info = string.Format("Houve um erro ao atualizar a parcela.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (DbUpdateException ex)
                {
                    var info = string.Format("Houve um erro ao atualizar a parcela.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (Exception ex)
                {
                    var info = string.Format("Houve um erro ao atualizar a parcela.\r\nError: {0}\r\nMessage:{1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
            }
        }
    }
}
