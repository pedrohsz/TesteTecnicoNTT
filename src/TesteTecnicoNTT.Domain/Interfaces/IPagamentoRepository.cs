using TesteTecnicoNTT.Domain.Entities;

namespace TesteTecnicoNTT.Domain.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<Pagamento> GetByIdAsync(Guid id);
        Task<IEnumerable<Pagamento>> GetByClienteIdAsync(Guid clienteId);
        Task AddAsync(Pagamento pagamento);
        Task UpdateAsync(Pagamento pagamento);
        Task DeleteAsync(Guid id);
    }
}
