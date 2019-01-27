using MediatR;
using Sgot.Service.Core.Commands;
using Sgot.Service.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Notifications
{
    public class AccountLogged : INotification
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public LoginResponse Login { get; private set; }

        public AccountLogged(bool success, string message = null, LoginResponse login = null)
        {
            Login = login;
            Message = message;
            IsSuccess = success;
        }
    }
}
