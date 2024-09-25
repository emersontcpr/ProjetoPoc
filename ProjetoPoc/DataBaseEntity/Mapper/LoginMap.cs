using DataBaseEntity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LoginMap : IEntityTypeConfiguration<Login>
{
    public void Configure(EntityTypeBuilder<Login> builder)
    {
        // Definindo o nome da tabela
        builder.ToTable("Login");

        // Configurando a chave primária
        builder.HasKey(l => l.Id);

        // Configurando as colunas
        builder.Property(l => l.NomeLogin)
            .HasColumnName("Login")
            .HasMaxLength(50)
            .IsRequired(); // Login é obrigatório

        builder.Property(l => l.Senha)
            .HasMaxLength(100)
            .IsRequired(); // Senha é obrigatória

        builder.Property(l => l.StatusUsuario)
            .IsRequired(); // Status é obrigatório
    }
}
