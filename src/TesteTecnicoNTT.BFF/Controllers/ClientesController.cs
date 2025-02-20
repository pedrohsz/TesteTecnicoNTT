using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace TesteTecnicoNTT.BFF.Controllers
{
    [Route("bff/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ClientesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Gateway");
        }

        // Apenas um exemplo de um endpoint protegido
        [Authorize]
        [HttpGet("protegido")]
        public IActionResult EndpointProtegido()
        {
            return Ok("Você acessou um endpoint protegido!");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente([FromBody] object command)
        {
            var response = await _httpClient.PostAsync("/clientes",
                new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] object command)
        {
            var response = await _httpClient.PutAsync($"/clientes/{id}",
                new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCliente(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/clientes/{id}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/clientes/{id}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            var response = await _httpClient.GetAsync("/clientes");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
