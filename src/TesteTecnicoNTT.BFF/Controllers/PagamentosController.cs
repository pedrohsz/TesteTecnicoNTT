using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace TesteTecnicoNTT.BFF.Controllers
{
    [Route("bff/pagamentos")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PagamentosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Gateway");
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPagamento([FromBody] object command)
        {
            var response = await _httpClient.PostAsync("/pagamentos",
                new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPagamento(Guid id, [FromBody] object command)
        {
            var response = await _httpClient.PutAsync($"/pagamentos/{id}",
                new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json"));

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirPagamento(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/pagamentos/{id}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPagamentoPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/pagamentos/{id}");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
