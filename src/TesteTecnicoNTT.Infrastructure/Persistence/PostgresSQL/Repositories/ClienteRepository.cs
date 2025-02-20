using Microsoft.EntityFrameworkCore;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly PostgresDbContext _context;

        public ClienteRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cliente?> ObterPorCpfCnpjAsync(string cpfCnpj)
        {
            return await _context.Clientes
                .Where(c => c.CpfCnpj == cpfCnpj)
                .FirstOrDefaultAsync();
        }
    }
}
