using MediatR;
using Sgot.Service.Core.Responses;

namespace Sgot.Service.Core.Commands.AccountRequest
{
    public class ResetPasswordAccount : IRequest<ResetPasswordResponse>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public ResetPasswordAccount(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
