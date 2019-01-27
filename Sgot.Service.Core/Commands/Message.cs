using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Commands
{
    public class Message
    {
        public DateTime DateCreated { get; protected set; }
        public string MessageId { get; protected set; }        

        public Message()
        {
            DateCreated = DateTime.UtcNow;
            MessageId = Guid.NewGuid().ToString();
        }
    }
}
