using DataBaseEntity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LogradouroMap : IEntityTypeConfiguration<Logradouro>
{
    public void Configure(EntityTypeBuilder<Logradouro> builder)
    {
        // Definindo o nome da tabela
        builder.ToTable("Logradouro");

        // Configurando a chave primária
        builder.HasKey(l => l.Id);

        // Configurando as colunas
        builder.Property(l => l.Descricao)
            .HasMaxLength(255)
            .IsRequired(); // Descrição é obrigatória

        // Configurando a chave estrangeira
        builder.Property(l => l.IdCliente).HasColumnName("Id_Cliente")
            .IsRequired();

        builder.HasOne(l => l.Cliente)
            .WithMany(c => c.Logradouros)
            .HasForeignKey(l => l.IdCliente);
    }
}
