using MediatR;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.FaturaRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles.FaturaHandler
{
    public class CreateFaturaHandle : IRequestHandler<CreateFatura, EntityResponse>
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;
        private readonly IParcelaService _parcelaService;
        private readonly IFaturaService _faturaService;

        public CreateFaturaHandle(IPedidoService pedidoService, IClienteService clienteService, IParcelaService parcelaService, IFaturaService faturaService)
        {
            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _parcelaService = parcelaService;
            _faturaService = faturaService;
        }

        public async Task<EntityResponse> Handle(CreateFatura request, CancellationToken cancellationToken)
        {
            try
            {
                var faturaCreated = await _faturaService.Post(request.Fatura).ConfigureAwait(false);
                var info = string.Format("Fatura {0} criada com sucesso", faturaCreated.Id);
                var response = new EntityResponse(true, false, false, faturaCreated, info, request);
                return await Task.FromResult(response);
            }
            catch (InvalidOperationException ex)
            {
                var info = string.Format("Houve um erro ao cadastrar a fatura.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, request.Fatura, info, request));
            }
            catch (Exception ex)
            {
                var info = string.Format("Houve um erro ao cadastrar a fatura.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, request.Fatura, info, request));
            }
        }
    }
}
