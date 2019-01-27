using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Service.Core.Commands.FaturaRequest;
using Sgot.Service.ViewModels;
using System.Threading.Tasks;

namespace Sgot.Application.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Faturas")]
    public class FaturasController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IParcelaRepository _parcelaRepository;
        private readonly IFaturaRepository _faturaRepository;

        public FaturasController(
            IPedidoRepository pedidoRepository, IClienteRepository clienteRepository,
            IParcelaRepository parcelaRepository, IFaturaRepository faturaRepository,
            IMapper mapper, IMediator mediator
            )
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _parcelaRepository = parcelaRepository;
            _faturaRepository = faturaRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET api/faturas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var faturas = await _faturaRepository.GetAllAsync().ConfigureAwait(false);
                foreach (var fatura in faturas)
                {
                    fatura.Pedido = await _pedidoRepository.GetByIdAsync(fatura.PedidoId).ConfigureAwait(false);
                    fatura.Cliente = await _clienteRepository.GetByIdAsync(fatura.ClienteId).ConfigureAwait(false);
                    var parcelas = await _parcelaRepository.GetAsync(p => p.FaturaId.Equals(fatura.Id)).ConfigureAwait(false);

                    foreach (var p in parcelas)
                    {
                        fatura.Parcelas.Add(p);
                    }
                }
                return Ok(faturas.ProjectTo<FaturaViewModel>());
            }
            return Unauthorized();
        }

        // GET api/faturas
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var fatura = await _faturaRepository.GetByIdAsync(id);
                if (fatura == null)
                    return NotFound();

                fatura.Pedido = await _pedidoRepository.GetByIdAsync(fatura.PedidoId).ConfigureAwait(false);
                fatura.Cliente = await _clienteRepository.GetByIdAsync(fatura.ClienteId).ConfigureAwait(false);
                var parcelas = await _parcelaRepository.GetAsync(p => p.FaturaId.Equals(fatura.Id)).ConfigureAwait(false);

                foreach (var p in parcelas)
                    fatura.Parcelas.Add(p);

                return Ok(_mapper.Map<FaturaViewModel>(fatura));
            }
            return Unauthorized();
        }

        // POST api/faturas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FaturaViewModel fatura)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new CreateFatura(_mapper.Map<Fatura>(fatura))).ConfigureAwait(false);
                if (result.IsCreated)
                    return CreatedAtAction("Get", new { id = ((Fatura)result.Item).Id }, (Fatura)result.Item);
                return BadRequest(result);
            }
            return Unauthorized();
        }

        // PUT api/faturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]FaturaViewModel fatura)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new UpdateFatura(id, _mapper.Map<Fatura>(fatura))).ConfigureAwait(false);
                if (result.IsUpdated)
                    return CreatedAtAction("Get", new { id = ((Fatura)result.Item).Id }, (Fatura)result.Item);
                return BadRequest(result);
            }
            return Unauthorized();
        }

        // DELETE api/faturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new DeleteFatura(id)).ConfigureAwait(false);
                if (result.IsDeleted)
                    return Ok((Fatura)result.Item);
                return BadRequest(result);
            }
            return Unauthorized();
        }
    }
}