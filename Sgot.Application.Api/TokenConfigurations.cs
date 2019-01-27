using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sgot.Application.Api
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
