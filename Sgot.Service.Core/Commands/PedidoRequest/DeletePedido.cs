using MediatR;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.PedidoRequest
{
    public class DeletePedido : Command, IRequest<EntityResponse>
    {
        public long Id { get; private set; }        

        public DeletePedido(long id)
        {
            Id = id;
            Name = GetType().Name;
        }
    }
}
