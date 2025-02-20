using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteTecnicoNTT.Domain.Entities;

namespace TesteTecnicoNTT.Infrastructure.Persistence.PostgresSQL.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CpfCnpj)
                .IsRequired()
                .HasMaxLength(14);

            builder.HasIndex(c => c.CpfCnpj)
                .IsUnique();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.NumeroContrato)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.RendaBruta)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasMany(c => c.Pagamentos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId);
        }
    }
}
