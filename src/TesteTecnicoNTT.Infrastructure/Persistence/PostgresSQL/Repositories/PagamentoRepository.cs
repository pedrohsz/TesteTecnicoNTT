using Microsoft.EntityFrameworkCore;
using TesteTecnicoNTT.Domain.Entities;
using TesteTecnicoNTT.Domain.Interfaces;

namespace TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PostgresDbContext _context;

        public PagamentoRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<Pagamento> GetByIdAsync(Guid id)
        {
            return await _context.Pagamentos.FindAsync(id);
        }

        public async Task<IEnumerable<Pagamento>> GetByClienteIdAsync(Guid clienteId)
        {
            return await _context.Pagamentos
                .AsNoTracking()
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task AddAsync(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pagamento pagamento)
        {
            _context.Pagamentos.Update(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento != null)
            {
                _context.Pagamentos.Remove(pagamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
