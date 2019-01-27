using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.PedidoRequest
{
    public class CreatePedido : Command, IRequest<EntityResponse>
    {
        public Pedido Pedido { get; private set; }

        public CreatePedido(Pedido pedido)
        {
            Pedido = pedido;
            Name = GetType().Name;
        }
    }
}
