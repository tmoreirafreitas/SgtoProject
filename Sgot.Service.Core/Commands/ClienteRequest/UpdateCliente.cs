using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.ClienteRequest
{
    public class UpdateCliente : Command, IRequest<EntityResponse>
    {
        public long Id { get; private set; }
        public Cliente Cliente { get; set; }

        public UpdateCliente(long id, Cliente cliente)
        {
            Id = id;
            Cliente = cliente;
            Name = GetType().Name;
        }
    }
}
