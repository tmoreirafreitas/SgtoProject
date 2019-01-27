using MediatR;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.ParcelaRequest
{
    public class DeleteParcela : Command, IRequest<EntityResponse>
    {
        public long Id { get; set; }

        public DeleteParcela(long id)
        {
            Id = id;
            Name = GetType().Name;
        }
    }
}
