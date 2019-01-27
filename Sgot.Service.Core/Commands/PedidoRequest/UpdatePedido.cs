using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.PedidoRequest
{
    public class UpdatePedido : Command, IRequest<EntityResponse>
    {
        public long PedidoId { get; private set; }
        public Pedido PedidoUpdate { get; private set; }

        public UpdatePedido(long pedidoId, Pedido pedidoUpdate)
        {
            PedidoId = pedidoId;
            PedidoUpdate = pedidoUpdate;
            Name = GetType().Name;
        }
    }
}
