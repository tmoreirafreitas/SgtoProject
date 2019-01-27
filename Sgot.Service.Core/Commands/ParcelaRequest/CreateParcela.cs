using MediatR;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Commands.ParcelaRequest
{
    public class CreateParcela : Command, IRequest<EntityResponse>
    {
        public Parcela Parcela { get; private set; }

        public CreateParcela(Parcela parcela)
        {
            Parcela = parcela;
            Name = GetType().Name;
        }
    }
}
