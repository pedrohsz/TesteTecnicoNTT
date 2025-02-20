using TesteTecnicoNTT.Domain.Entities;

namespace TesteTecnicoNTT.Domain.Interfaces
{
    public interface IClienteReadRepository
    {
        Task<List<Cliente>> ObterTodosClientesAsync();
        Task<Cliente> ObterPorIdAsync(Guid id);
        Task<decimal> ObterMediaRendaBrutaPorEstadoAsync(string estado);
    }
}
