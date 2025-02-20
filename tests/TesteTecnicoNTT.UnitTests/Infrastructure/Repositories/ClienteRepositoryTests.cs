using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Repositories;

namespace TesteTecnicoNTT.UnitTests.Infrastructure.Repositories
{
    public class ClienteRepositoryTests : TestBase
    {
        private readonly ClienteRepository _repository;

        public ClienteRepositoryTests()
        {
            _repository = new ClienteRepository(_context);
        }

        [Fact]
        public async Task Deve_Adicionar_Cliente_Corretamente()
        {
            // Arrange
            var cliente = new Cliente("12345678901", "João Teste", "CTR-0001", "São Paulo", "SP", 5000.00m);

            // Act
            await _repository.AddAsync(cliente);
            var clienteEncontrado = await _repository.GetByIdAsync(cliente.Id);

            // Assert
            Assert.NotNull(clienteEncontrado);
            Assert.Equal("João Teste", clienteEncontrado.Nome);
        }

        [Fact]
        public async Task Deve_Atualizar_Cliente_Corretamente()
        {
            // Arrange
            var cliente = new Cliente("12345678902", "Maria Teste", "CTR-0002", "Rio de Janeiro", "RJ", 7000.00m);
            await _repository.AddAsync(cliente);

            // Act
            cliente.AtualizarNome("Maria Silva");
            await _repository.UpdateAsync(cliente);

            var clienteAtualizado = await _repository.GetByIdAsync(cliente.Id);

            // Assert
            Assert.NotNull(clienteAtualizado);
            Assert.Equal("Maria Silva", clienteAtualizado.Nome);
        }

        [Fact]
        public async Task Deve_Excluir_Cliente_Corretamente()
        {
            // Arrange
            var cliente = new Cliente("12345678903", "Carlos Teste", "CTR-0003", "Curitiba", "PR", 4000.00m);
            await _repository.AddAsync(cliente);

            // Act
            await _repository.DeleteAsync(cliente.Id);
            var clienteEncontrado = await _repository.GetByIdAsync(cliente.Id);

            // Assert
            Assert.Null(clienteEncontrado);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Se_Cliente_Nao_Existir()
        {
            // Arrange
            var idInexistente = Guid.NewGuid();

            // Act
            var clienteEncontrado = await _repository.GetByIdAsync(idInexistente);

            // Assert
            Assert.Null(clienteEncontrado);
        }
    }
}
