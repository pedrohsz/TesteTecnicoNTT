using TesteTecnicoNTT.Domain.Entities;

namespace TesteTecnicoNTT.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> GetByIdAsync(Guid id);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Guid id);
        Task<Cliente?> ObterPorCpfCnpjAsync(string cpfCnpj);
    }
}