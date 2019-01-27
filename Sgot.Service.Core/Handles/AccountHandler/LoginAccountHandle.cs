using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Commands.AccountRequest;
using Sgot.Service.Core.Notifications;
using Sgot.Service.Core.Responses;
using Sgot.Service.Core.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles
{
    public class LoginAccountHandle : IRequestHandler<LoginAccount, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginAccountHandle> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public LoginAccountHandle(IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginAccountHandle> logger,
            IMediator mediator)
        {
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<LoginResponse> Handle(LoginAccount request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName).ConfigureAwait(false);
                // check if username exists
                if (user == null)
                {
                    await _mediator.Publish(new AccountLogged(false, "Falha ao logar no sistema"));
                }

                // check if password is correct            
                if (!await _userManager.CheckPasswordAsync(user, request.Password).ConfigureAwait(false))
                {
                    await _mediator.Publish(new AccountLogged(false, "Falha ao logar no sistema"));
                }

                var token = Util.CreateToken(user, _userManager, _configuration);
                var login = new LoginResponse(user.Id, user.UserName, true, token, "OK");
                await _mediator.Publish(new AccountLogged(true, string.Format("Usuário {0} logou no sistema", login.Id), login));
                return await Task.FromResult(login);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
