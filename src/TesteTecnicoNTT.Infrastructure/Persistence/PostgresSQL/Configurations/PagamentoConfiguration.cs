using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteTecnicoNTT.Domain.Entities;

namespace TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Configurations
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamentos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ClienteId)
                .IsRequired();

            //builder.HasOne<Cliente>()
            //    .WithMany()
            //    .HasForeignKey(p => p.ClienteId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.Pagamentos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.NumeroContrato)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Parcela)
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.DataPagamento)
                .IsRequired();

            builder.Property(p => p.Status)
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
