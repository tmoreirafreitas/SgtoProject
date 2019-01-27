using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.FaturaRequest
{
    public class UpdateFatura : Command, IRequest<EntityResponse>
    {
        public long Id { get; set; }
        public Fatura Fatura { get; set; }

        public UpdateFatura(long id, Fatura fatura)
        {
            Id = id;
            Fatura = fatura;
            Name = GetType().Name;
        }
    }
}
