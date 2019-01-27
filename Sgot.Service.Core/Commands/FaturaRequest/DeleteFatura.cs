using MediatR;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Commands.FaturaRequest
{
    public class DeleteFatura : Command, IRequest<EntityResponse>
    {
        public long Id { get; set; }

        public DeleteFatura(long id)
        {
            Id = id;
            Name = GetType().Name;
        }
    }
}
