using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace TesteTecnicoNTT.IntegrationTests.API.Controllers
{
    public class ClientesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ClientesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> CriarClienteAsync(object cliente)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/clientes", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(responseContent)?.Trim('"') ?? string.Empty;
        }

        [Fact]
        public async Task Deve_Criar_Cliente_Com_Sucesso()
        {
            // Arrange
            var cliente = new
            {
                CpfCnpj = "12345678901",
                Nome = "João Teste",
                NumeroContrato = "CTR-0001",
                Cidade = "São Paulo",
                Estado = "SP",
                RendaBruta = 5000.00m
            };

            // Act
            var clienteId = await CriarClienteAsync(cliente);

            // Assert
            Assert.False(string.IsNullOrEmpty(clienteId));
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Ao_Criar_Cliente_Sem_CpfCnpj()
        {
            // Arrange
            var cliente = new
            {
                Nome = "Cliente Sem CPF",
                NumeroContrato = "CTR-0002",
                Cidade = "Rio de Janeiro",
                Estado = "RJ",
                RendaBruta = 7000.00m
            };

            // Act
            var response = await _client.PostAsync("/api/clientes", new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Deve_Retornar_Cliente_Existente()
        {
            // Arrange
            var cliente = new
            {
                CpfCnpj = "12345678902",
                Nome = "Cliente Teste",
                NumeroContrato = "CTR-1234",
                Cidade = "Curitiba",
                Estado = "PR",
                RendaBruta = 6000.00m
            };

            var clienteId = await CriarClienteAsync(cliente);

            // Act
            var response = await _client.GetAsync($"/api/clientes/{clienteId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Se_Cliente_Nao_Existir()
        {
            // Arrange
            var clienteId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/clientes/{clienteId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Deve_Excluir_Cliente_Com_Sucesso()
        {
            // Arrange
            var cliente = new
            {
                CpfCnpj = "12345678903",
                Nome = "Cliente para Exclusão",
                NumeroContrato = "CTR-9999",
                Cidade = "Brasília",
                Estado = "DF",
                RendaBruta = 5000.00m
            };

            var clienteId = await CriarClienteAsync(cliente);

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/clientes/{clienteId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }
    }
}
