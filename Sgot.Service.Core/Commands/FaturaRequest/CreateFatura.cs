using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Commands.FaturaRequest
{
    public class CreateFatura : Command, IRequest<EntityResponse>
    {
        public Fatura Fatura { get; set; }

        public CreateFatura(Fatura fatura)
        {
            Fatura = fatura;
            Name = GetType().Name;
        }
    }
}
