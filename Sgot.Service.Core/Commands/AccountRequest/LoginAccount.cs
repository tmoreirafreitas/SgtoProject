using MediatR;
using Sgot.Service.Core.Responses;
using System;

namespace Sgot.Service.Core.Commands.AccountRequest
{
    public class LoginAccount : IRequest<LoginResponse>
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public LoginAccount(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
