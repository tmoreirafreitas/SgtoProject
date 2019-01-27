using MediatR;
using Sgot.Domain.Interfaces.Services;
using Sgot.Service.Core.Commands.ParcelaRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles.ParcelaHandler
{
    public class CreateParcelaHandle : IRequestHandler<CreateParcela, EntityResponse>
    {
        private readonly IParcelaService _parcelaService;        

        public CreateParcelaHandle(IParcelaService parcelaService)
        {
            _parcelaService = parcelaService;            
        }

        public async Task<EntityResponse> Handle(CreateParcela request, CancellationToken cancellationToken)
        {
            try
            {
                var parcelaCreated = await _parcelaService.Post(request.Parcela);
                var info = string.Format("Parcela {0} criado com sucesso", parcelaCreated.Id);
                var response = new EntityResponse(true, false, false, parcelaCreated, info, request);
                return await Task.FromResult(response);
            }
            catch (InvalidOperationException ex)
            {
                var info = string.Format("Houve um erro ao cadastrar a parcela.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
            catch (Exception ex)
            {
                var info = string.Format("Houve um erro ao cadastrar a parcela.\r\nErro: {0}\r\nMessage: {1}",
                    ex.InnerException.StackTrace, ex.InnerException.Message);
                return await Task.FromResult(new EntityResponse(false, false, false, null, info, request));
            }
        }
    }
}
