using CadastroEscolar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroEscolar.Infra.Data.Mappings;

public class AlunoMapping : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome);
        builder.Property(x => x.CriadoEm);
        builder.Property(x => x.Mensalidade).HasColumnType("decimal(18,2)");
        builder.Property(x => x.DataVencimento);
    }
}