using MediatR;
using Sgot.Service.Core.Responses;
using System.Collections.Generic;
using System.Security.Claims;

namespace Sgot.Service.Core.Commands.AccountRequest
{
    public class RegisterAccount : IRequest<LoginResponse>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public IList<Claim> Claims { get; private set; }

        public RegisterAccount(string email, string password, string confirmPassword, string fullName, string userName)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            FullName = fullName;
            UserName = userName;
            Claims = new List<Claim>();
        }
    }
}
