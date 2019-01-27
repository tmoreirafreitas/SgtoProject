using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Service.Core.Commands.PedidoRequest;
using Sgot.Service.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sgot.Application.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Pedidos")]
    public class PedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IOculosRepository _oculosRepository;
        private readonly ILenteRepository _lenteRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PedidosController(IPedidoRepository pedidoRepository, IOculosRepository oculosRepository,
            ILenteRepository lenteRepository, IClienteRepository clienteRepository,
            IMapper mapper, IMediator mediator)
        {
            _pedidoRepository = pedidoRepository;
            _oculosRepository = oculosRepository;
            _lenteRepository = lenteRepository;
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET api/pedidos
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                try
                {
                    var pedidos = await _pedidoRepository.GetAllAsync().ConfigureAwait(false);
                    if (pedidos == null)
                        return NotFound();
                    foreach (var pedido in pedidos)
                    {
                        var cliente = await _clienteRepository.GetByIdAsync(pedido.ClienteId);
                        pedido.Cliente = cliente;

                        var oculos = await _oculosRepository.GetAsync(o => o.PedidoId.Equals(pedido.Id)).ConfigureAwait(false);
                        foreach (var o in oculos)
                        {
                            pedido.Oculos.Add(o);
                        }

                        foreach (var ocls in oculos)
                        {
                            var lentes = await _lenteRepository.GetAsync(l => l.OculosId.Equals(ocls.Id)).ConfigureAwait(false);
                            if (lentes != null && lentes.ToList().Count == 2)
                            {
                                ocls.Lentes.Add(lentes.ToList()[0]);
                                ocls.Lentes.Add(lentes.ToList()[1]);
                            }
                        }
                    }
                    return Ok(pedidos.ProjectTo<PedidoViewModel>());
                }
                catch (Exception ex)
                {
                    var info = string.Format("Error: {0}\r\nMessage; {1}\r\n", ex.InnerException.StackTrace, ex.InnerException.Message);
                    return BadRequest(info);
                }
            }
            return Unauthorized();
        }

        // GET api/pedidos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
                {
                    var pedido = await _pedidoRepository.GetByIdAsync(id).ConfigureAwait(false);
                    if (pedido == null)
                        return NotFound();

                    var cliente = await _clienteRepository.GetByIdAsync(pedido.ClienteId);
                    pedido.Cliente = cliente;

                    var oculos = await _oculosRepository.GetAsync(o => o.PedidoId.Equals(pedido.Id)).ConfigureAwait(false);
                    foreach (var o in oculos)
                    {
                        pedido.Oculos.Add(o);
                    }

                    foreach (var ocls in oculos)
                    {
                        var lentes = await _lenteRepository.GetAsync(l => l.OculosId.Equals(ocls.Id)).ConfigureAwait(false);
                        if (lentes != null && lentes.ToList().Count == 2)
                        {
                            ocls.Lentes.Add(lentes.ToList()[0]);
                            ocls.Lentes.Add(lentes.ToList()[1]);
                        }
                    }
                    return Ok(_mapper.Map<PedidoViewModel>(pedido));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                var info = string.Format("Error: {0}\r\nMessage; {1}\r\n", ex.InnerException.StackTrace, ex.InnerException.Message);
                return BadRequest(info);
            }
        }

        // POST api/pedidos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PedidoViewModel pedido)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new CreatePedido(_mapper.Map<Pedido>(pedido)));
                if (result.IsCreated)
                    return CreatedAtAction("Get", new { id = result.Item.Id }, (Pedido)result.Item);
                return BadRequest(result);
            }
            return Unauthorized();
        }

        // PUT api/pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]PedidoViewModel pedido)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var pedidoToUpdate = _mapper.Map<Pedido>(pedido);
                var responseResult = await _mediator.Send(new UpdatePedido(id, pedidoToUpdate));
                if (responseResult.IsUpdated)
                    return Ok(_mapper.Map<PedidoViewModel>((Pedido)responseResult.Item));
                return BadRequest(responseResult);
            }
            return Unauthorized();
        }

        // DELETE api/pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var responseResult = await _mediator.Send(new DeletePedido(id));
                if (responseResult.IsDeleted)
                    return Ok(_mapper.Map<PedidoViewModel>((Pedido)responseResult.Item));
                return BadRequest(responseResult);
            }
            return Unauthorized();
        }
    }
}