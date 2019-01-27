using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Commands
{
    public class Command : Message
    {
        public string Name { get; protected set; }        
        public Command()
        {
            
        }
    }
}
