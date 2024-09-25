using DataBaseEntity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClienteMap : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        // Definindo o nome da tabela
        builder.ToTable("Clientes");

        // Configurando a chave primária
        builder.HasKey(c => c.Id);

        // Configurando as colunas
        builder.Property(c => c.Nome)
            .HasMaxLength(50)
            .IsRequired(); // Nome é obrigatório

        builder.Property(c => c.Email)
            .HasMaxLength(100)
            .IsRequired(); // Email é obrigatório

        builder.Property(c => c.Logotipo)
            .HasColumnType("VARBINARY(MAX)");

        // Relacionamento: 1 Cliente -> Muitos Logradouros
        builder.HasMany(c => c.Logradouros)
            .WithOne(l => l.Cliente)
            .HasForeignKey(l => l.IdCliente)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
