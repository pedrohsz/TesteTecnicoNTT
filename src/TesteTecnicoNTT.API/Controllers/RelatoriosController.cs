using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoNTT.Application.Pagamentos.Queries;

namespace TesteTecnicoNTT.API.Controllers
{
    [Route("api/pagamentos/relatorios")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("total-pagamentos")]
        public async Task<IActionResult> ObterTotalPagamentos()
        {
            var resultado = await _mediator.Send(new ObterTotalPagamentosQuery());
            return Ok(resultado);
        }

        [HttpGet("atrasados-avencer")]
        public async Task<IActionResult> ObterTotalPagamentosAtrasadosOuAVencer()
        {
            var resultado = await _mediator.Send(new ObterTotalPagamentosAtrasadosOuAVencerQuery());
            return Ok(resultado);
        }

        [HttpGet("por-estado")]
        public async Task<IActionResult> ObterPagamentosPorEstado()
        {
            var resultado = await _mediator.Send(new ObterPagamentosPorEstadoQuery());
            return Ok(resultado);
        }

        [HttpGet("media-renda-status")]
        public async Task<IActionResult> ObterMediaRendaPorStatus()
        {
            var resultado = await _mediator.Send(new ObterMediaRendaPorEstadoQuery());
            return Ok(resultado);
        }

        [HttpGet("clientes-por-estado-status")]
        public async Task<IActionResult> ObterClientesPorEstadoEStatus()
        {
            var resultado = await _mediator.Send(new ObterClientesPorEstadoEStatusQuery());
            return Ok(resultado);
        }
    }
}
