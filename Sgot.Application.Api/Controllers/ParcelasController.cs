using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Service.Core.Commands.ParcelaRequest;
using Sgot.Service.ViewModels;
using System.Threading.Tasks;

namespace Sgot.Application.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Parcelas")]
    public class ParcelasController : Controller
    {
        private readonly IParcelaRepository _parcelaRepository;
        private readonly IFaturaRepository _faturaRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ParcelasController(IParcelaRepository parcelaRepository, IFaturaRepository faturaRepository, IMapper mapper, IMediator mediator)
        {
            _parcelaRepository = parcelaRepository;
            _faturaRepository = faturaRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        // GET api/parcelas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var parcelas = await _parcelaRepository.GetAllAsync().ConfigureAwait(false);
                foreach (var p in parcelas)
                {
                    p.Fatura = await _faturaRepository.GetByIdAsync(p.FaturaId);
                }
                return Ok(parcelas.ProjectTo<ParcelaViewModel>());
            }
            return Unauthorized();
        }

        // GET api/parcelas
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var parcela = await _parcelaRepository.GetByIdAsync(id).ConfigureAwait(false);
                if (parcela == null)
                    return NotFound();

                parcela.Fatura = await _faturaRepository.GetByIdAsync(parcela.FaturaId);
                return Ok(_mapper.Map<ParcelaViewModel>(parcela));
            }
            return Unauthorized();
        }

        // POST api/parcelas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ParcelaViewModel parcela)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new CreateParcela(_mapper.Map<Parcela>(parcela))).ConfigureAwait(false);
                if (result.IsCreated)
                    return CreatedAtAction("Get", new { id = ((Parcela)result.Item).Id }, (Parcela)result.Item);
                return BadRequest(result);
            }
            return Unauthorized();
        }

        // PUT api/parcelas
        [HttpPut]
        public async Task<IActionResult> Put(long id, [FromBody]ParcelaViewModel parcela)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new UpdateParcela(id, _mapper.Map<Parcela>(parcela))).ConfigureAwait(false);
                if (result.IsUpdated)
                    return Ok((Parcela)result.Item);
                return BadRequest(result);
            }
            return Unauthorized();
        }

        // DELETE api/parcelas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (HttpContext.User.HasClaim(c => c.Type.Equals(ClaimType.CEO.ToString()) || c.Type.Equals(ClaimType.VEND.ToString()) || c.Type.Equals(ClaimType.ADMIN.ToString())))
            {
                var result = await _mediator.Send(new DeleteParcela(id)).ConfigureAwait(false);
                if (result.IsDeleted)
                    return Ok(result);
                return BadRequest(result);
            }
            return Unauthorized();
        }
    }
}