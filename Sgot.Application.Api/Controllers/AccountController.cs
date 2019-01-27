using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sgot.Application.Api.ViewModels.AccountViewModels;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Commands.AccountRequest;
using Sgot.Service.Core.Utils;
using Sgot.Service.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sgot.Application.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            IMapper mapper, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                var result = await _mediator.Send(new LoginAccount(model.UserName, model.Password)).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _mediator.Send(new RegisterAccount(model.Email, model.Password, model.ConfirmPassword, model.FullName, model.UserName)).ConfigureAwait(false);
                    if (result != null && result.Authenticated)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            result.Authenticated,
                            result.Message
                        });
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }
                var result = await _mediator.Send(new ResetPasswordAccount(model.Email, model.Password)).ConfigureAwait(false);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users;
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);
                foreach (var claim in claims)
                {
                    user.Claims.Add(claim);
                }
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                }
            }
            return Ok(users);
        }

        [HttpGet]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            _logger.LogInformation($"'{DateTime.UtcNow} ' - Carregando usuário.");
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);

                var login = _mapper.Map<LoginViewModel>(user);
                foreach (var claim in claims)
                {
                    user.Claims.Add(claim);
                    login.Claims.Add(claim);
                }
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                    login.Roles.Add(role);
                }
                return Ok(login);
            }
            _logger.LogInformation($"'{DateTime.UtcNow} ' - Usuário não foi encontrado na base de dados.");
            return NotFound();
        }

        [HttpGet]
        [Route("user/{username}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            _logger.LogInformation($"'{DateTime.UtcNow} ' - Carregando usuário.");
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var claims = await _userManager.GetClaimsAsync(user);
                var login = _mapper.Map<LoginViewModel>(user);
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                    login.Roles.Add(role);
                }

                return Ok(login);
            }
            _logger.LogInformation($"'{DateTime.UtcNow} ' - Usuário não foi encontrado na base de dados.");
            return NotFound();
        }

        [HttpGet]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.FindByNameAsync(
                    User.Identity.Name ??
                    User.Claims.Where(c => c.Properties.ContainsKey("unique_name")).Select(c => c.Value).FirstOrDefault()
                    );

                    var tokken = Util.CreateToken(user, _userManager, _configuration);
                    DateTime dataCriacao = DateTime.Now;
                    DateTime dataExpiracao = dataCriacao +
                        TimeSpan.FromSeconds(int.Parse(_configuration.GetValue<string>("TokenConfigurations:Seconds")));
                    var response = new
                    {
                        id = user.Id,
                        username = user.UserName,
                        authenticated = true,
                        created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                        expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                        accessToken = tokken,
                        message = "OK"
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}