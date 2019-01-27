using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Responses
{
    public class LoginErroResponse
    {
        public bool Authenticated { get; private set; }
        public string Message { get; private set; }
        public IEnumerable<IdentityResult> Result { get; private set; }

        public LoginErroResponse(bool authenticated, string message)
        {
            Authenticated = authenticated;
            Message = message;
            Result = new List<IdentityResult>();
        }     
    }
}
