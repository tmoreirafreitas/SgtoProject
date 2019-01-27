using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Service.Core.Commands.ClienteRequest;
using Sgot.Service.ViewModels;
using System;
using System.Threading.Tasks;

namespace Sgot.Application.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ClientesController(IClienteRepository clienteRepository, IMapper mapper, IMediator mediator)
        {
            _clienteRepository = clienteRepository;            
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET api/clientes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                try
                {
                    var clientes = await _clienteRepository.GetAllAsync().ConfigureAwait(false);                    
                    return Ok(clientes.ProjectTo<ClienteViewModel>());
                }
                catch (Exception ex)
                {
                    var info = string.Format("Error: {0}\r\nMessage; {1}\r\n", ex.InnerException.StackTrace, ex.InnerException.Message);
                    return BadRequest(info);
                }
            }
            return Unauthorized();
        }

        // GET api/clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                try
                {
                    var cliente = await _clienteRepository.GetByIdAsync(id).ConfigureAwait(false);
                    if (cliente == null)
                        return NotFound();                    
                    return Ok(_mapper.Map<ClienteViewModel>(cliente));

                }
                catch (Exception ex)
                {
                    var info = string.Format("Error: {0}\r\nMessage; {1}\r\n", ex.InnerException.StackTrace, ex.InnerException.Message);
                    return BadRequest(info);
                }
            }
            return Unauthorized();
        }

        // POST api/clientes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ClienteViewModel clienteViewModel)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var cliente = _mapper.Map<Cliente>(clienteViewModel);
                var result = await _mediator.Send(new CreateCliente(cliente));
                if (result.IsCreated)
                    return CreatedAtAction("Get", new { id = ((Cliente)result.Item).Id }, (Cliente)result.Item);
                return BadRequest(result);
            }
            return Unauthorized();
        }

        // PUT api/clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ClienteViewModel cliente)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var clienteToUpdate = _mapper.Map<Cliente>(cliente);
                var result = await _mediator.Send(new UpdateCliente(id, clienteToUpdate)).ConfigureAwait(false);
                if (result.IsUpdated)
                    return Ok(_mapper.Map<ClienteViewModel>((Cliente)result.Item));
                return BadRequest(result);
            }
            return Unauthorized();
        }

        // DELETE api/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new DeleteCliente(id));
                if (result.IsDeleted)
                    return Ok(result);
                return BadRequest(result);
            }
            return Unauthorized();
        }
    }
}