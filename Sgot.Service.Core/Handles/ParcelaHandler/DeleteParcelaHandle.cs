using MediatR;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.ParcelaRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sgot.Service.Core.Handles.ParcelaHandler
{
    public class DeleteParcelaHandle : IRequestHandler<DeleteParcela, EntityResponse>
    {
        private readonly IParcelaService _parcelaService;
        private readonly IParcelaRepository _parcelaRepository;

        public DeleteParcelaHandle(IParcelaService parcelaService, IParcelaRepository parcelaRepository)
        {
            _parcelaService = parcelaService;
            _parcelaRepository = parcelaRepository;
        }

        public async Task<EntityResponse> Handle(DeleteParcela request, CancellationToken cancellationToken)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var hasParcela = await _parcelaRepository.GetByIdAsync(request.Id).ConfigureAwait(false);
                    if (hasParcela == null)
                        return await Task.FromResult(new EntityResponse(false, false, false, null, "Parcela inexistente.", request));

                    await _parcelaService.Delete(hasParcela.Id).ConfigureAwait(false);
                    scope.Complete();
                    var msg = string.Format("A parcela ID: {0} cujo Número {1} foi deletada com sucesso",
                        hasParcela.Id, hasParcela.Numero);
                    return await Task.FromResult(new EntityResponse(false, false, true, null, msg, request));
                }
                catch (InvalidOperationException ex)
                {
                    var info = string.Format("Houve um erro ao deletar a parcela.\r\nErro: {0}\r\nMessage: {1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
                catch (Exception ex)
                {
                    var info = string.Format("Houve um erro ao deletar a parcela.\r\nErro: {0}\r\nMessage: {1}",
                        ex.InnerException.StackTrace, ex.InnerException.Message);
                    return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
                }
            }
        }
    }
}
