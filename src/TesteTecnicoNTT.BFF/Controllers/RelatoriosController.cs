using Microsoft.AspNetCore.Mvc;

namespace TesteTecnicoNTT.BFF.Controllers
{
    [Route("bff/pagamentos/relatorios")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public RelatoriosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Gateway");
        }

        [HttpGet("total-pagamentos")]
        public async Task<IActionResult> ObterTotalPagamentos()
        {
            var response = await _httpClient.GetAsync("/pagamentos/relatorios/total-pagamentos");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("atrasados-avencer")]
        public async Task<IActionResult> ObterTotalPagamentosAtrasadosOuAVencer()
        {
            var response = await _httpClient.GetAsync("/pagamentos/relatorios/atrasados-avencer");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("por-estado")]
        public async Task<IActionResult> ObterPagamentosPorEstado()
        {
            var response = await _httpClient.GetAsync("/pagamentos/relatorios/por-estado");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("media-renda-status")]
        public async Task<IActionResult> ObterMediaRendaPorStatus()
        {
            var response = await _httpClient.GetAsync("/pagamentos/relatorios/media-renda-status");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("clientes-por-estado-status")]
        public async Task<IActionResult> ObterClientesPorEstadoEStatus()
        {
            var response = await _httpClient.GetAsync("/pagamentos/relatorios/clientes-por-estado-status");
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
