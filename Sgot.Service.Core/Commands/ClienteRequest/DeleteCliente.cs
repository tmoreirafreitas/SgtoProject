using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Commands.ClienteRequest
{
    public class DeleteCliente : Command, IRequest<EntityResponse>
    {
        public long Id { get; private set; }

        public DeleteCliente(long id)
        {
            Id = id;
            Name = GetType().Name;
        }
    }
}
