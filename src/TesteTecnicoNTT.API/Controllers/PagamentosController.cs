namespace TesteTecnicoNTT.API.Controllers
{
    using global::TesteTecnicoNTT.Application.Pagamentos.Commands.Create;
    using global::TesteTecnicoNTT.Application.Pagamentos.Commands.Delete;
    using global::TesteTecnicoNTT.Application.Pagamentos.Commands.Update;
    using global::TesteTecnicoNTT.Application.Pagamentos.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    namespace TesteTecnicoNTT.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PagamentosController : ControllerBase
        {
            private readonly IMediator _mediator;

            public PagamentosController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost]
            public async Task<IActionResult> AdicionarPagamento([FromBody] AdicionarPagamentoCommand command)
            {
                var pagamentoId = await _mediator.Send(command);
                return CreatedAtAction(nameof(ObterPagamentoPorId), new { id = pagamentoId }, pagamentoId);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> AtualizarPagamento(Guid id, [FromBody] AtualizarPagamentoCommand command)
            {
                command.Id = id;
                var sucesso = await _mediator.Send(command);
                return sucesso ? Ok() : NotFound();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> ExcluirPagamento(Guid id)
            {
                var sucesso = await _mediator.Send(new ExcluirPagamentoCommand { Id = id });
                return sucesso ? Ok() : NotFound();
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> ObterPagamentoPorId(Guid id)
            {
                var pagamento = await _mediator.Send(new ObterPagamentoPorIdQuery { Id = id });
                return pagamento != null ? Ok(pagamento) : NotFound();
            }
        }
    }

}
