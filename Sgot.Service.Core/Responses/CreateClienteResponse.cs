using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Responses
{
    public class CreateClienteResponse
    {
        public long Id { get; private set; }

        public CreateClienteResponse(long id)
        {
            Id = id;
        }
    }
}
