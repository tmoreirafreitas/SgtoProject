using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.ParcelaRequest
{
    public class UpdateParcela:Command, IRequest<EntityResponse>
    {
        public long Id { get; set; }
        public Parcela Parcela { get; set; }

        public UpdateParcela(long id, Parcela parcela)
        {
            Id = id;
            Parcela = parcela;
            Name = GetType().Name;
        }
    }
}
