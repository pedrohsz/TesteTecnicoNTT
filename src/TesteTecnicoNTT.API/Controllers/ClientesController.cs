using MediatR;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoNTT.Application.Clientes.Commands.Create;
using TesteTecnicoNTT.Application.Clientes.Commands.Delete;
using TesteTecnicoNTT.Application.Clientes.Commands.Update;
using TesteTecnicoNTT.Application.Clientes.Queries;

namespace TesteTecnicoNTT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente([FromBody] AdicionarClienteCommand command)
        {
            var clienteId = await _mediator.Send(command);
            return Ok(clienteId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] AtualizarClienteCommand command)
        {
            command.Id = id;
            var sucesso = await _mediator.Send(command);
            return sucesso ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCliente(Guid id)
        {
            var sucesso = await _mediator.Send(new ExcluirClienteCommand { Id = id });
            return sucesso ? Ok() : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(Guid id)
        {
            var cliente = await _mediator.Send(new ObterClientePorIdQuery { Id = id });
            return cliente != null ? Ok(cliente) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            return Ok("OK");
        }
    }
}
