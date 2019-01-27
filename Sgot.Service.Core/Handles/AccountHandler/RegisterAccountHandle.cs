using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Commands.AccountRequest;
using Sgot.Service.Core.Responses;
using Sgot.Service.Core.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles
{
    public class RegisterAccountHandle : IRequestHandler<RegisterAccount, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;        
        private readonly ILogger<RegisterAccountHandle> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RegisterAccountHandle(UserManager<ApplicationUser> userManager,            
            ILogger<RegisterAccountHandle> logger,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;            
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<LoginResponse> Handle(RegisterAccount request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(request);
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var token = Util.CreateToken(user, _userManager, _configuration);
                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao +
                        TimeSpan.FromSeconds(int.Parse(_configuration.GetValue<string>("TokenConfigurations:Seconds")));

                    return await Task.FromResult(new LoginResponse(user.Id, user.UserName, true, token, "OK"));
                }
                return new LoginResponse(false, "Falha ao registrar");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
