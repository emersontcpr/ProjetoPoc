using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseEntity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TokenMap : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        // Definindo o nome da tabela
        builder.ToTable("Token");

        // Configurando a chave primária
        builder.HasKey(t => t.Id);

        // Configurando as colunas
        builder.Property(t => t.ValorToken)
            .HasColumnName("Token")
            .HasColumnType("varchar(MAX)")
            .IsRequired(); // Token é obrigatório

        builder.Property(t => t.DataCadastro)
            .IsRequired(); // Data de cadastro é obrigatória

        builder.Property(t => t.DataExpiracao)
            .IsRequired(); // Data de expiração é obrigatória
    }
}
