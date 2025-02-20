using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace TesteTecnicoNTT.IntegrationTests.API.Controllers
{
    public class PagamentosControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PagamentosControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> CriarPagamentoAsync(object pagamento)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pagamento), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/pagamentos", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(responseContent)?.Trim('"') ?? string.Empty;
        }

        [Fact]
        public async Task Deve_Criar_Pagamento_Com_Sucesso()
        {
            // Arrange
            var pagamento = new
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-2001",
                Parcela = 1,
                Valor = 1500.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = 1 // Pago
            };

            // Act
            var pagamentoId = await CriarPagamentoAsync(pagamento);

            // Assert
            Assert.False(string.IsNullOrEmpty(pagamentoId));
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Ao_Criar_Pagamento_Com_Valor_Invalido()
        {
            // Arrange
            var pagamento = new
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-2002",
                Parcela = 1,
                Valor = -100, // Valor inválido
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = 1
            };

            var content = new StringContent(JsonConvert.SerializeObject(pagamento), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/pagamentos", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Deve_Retornar_Pagamento_Existente()
        {
            // Arrange
            var pagamento = new
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-2003",
                Parcela = 1,
                Valor = 2000.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = 1
            };

            var pagamentoId = await CriarPagamentoAsync(pagamento);

            // Act
            var response = await _client.GetAsync($"/api/pagamentos/{pagamentoId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Se_Pagamento_Nao_Existir()
        {
            // Arrange
            var pagamentoId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/pagamentos/{pagamentoId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Deve_Excluir_Pagamento_Com_Sucesso()
        {
            // Arrange
            var pagamento = new
            {
                ClienteId = Guid.NewGuid(),
                NumeroContrato = "CTR-9999",
                Parcela = 1,
                Valor = 5000.00m,
                DataPagamento = DateTime.UtcNow.AddDays(5),
                Status = 1
            };

            var pagamentoId = await CriarPagamentoAsync(pagamento);

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/pagamentos/{pagamentoId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }
    }
}
