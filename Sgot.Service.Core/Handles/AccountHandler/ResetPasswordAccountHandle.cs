using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Commands.AccountRequest;
using Sgot.Service.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles
{
    public class ResetPasswordAccountHandle : IRequestHandler<ResetPasswordAccount, ResetPasswordResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ResetPasswordAccountHandle> _logger;        

        public ResetPasswordAccountHandle(UserManager<ApplicationUser> userManager,
            ILogger<ResetPasswordAccountHandle> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordAccount request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return new ResetPasswordResponse();
                }
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, code, request.Password);
                if (result.Succeeded)
                {
                    return new ResetPasswordResponse();
                }
                return new ResetPasswordResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
