using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Responses
{
    public class DeleteClienteResponse
    {
        public bool Deleted { get; private set; }
        public string Message { get; private set; }

        public DeleteClienteResponse(bool deleted, string message)
        {
            Deleted = deleted;
            Message = message;
        }
    }
}
