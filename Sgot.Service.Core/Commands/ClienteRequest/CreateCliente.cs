using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.ClienteRequest
{
    public class CreateCliente : Command, IRequest<EntityResponse>
    {
        public Cliente Cliente { get; private set; }

        public CreateCliente(Cliente cliente)
        {
            Cliente = cliente;
            Name = GetType().Name;
        }
    }
}
