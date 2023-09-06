using CadastroEscolar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroEscolar.Infra.Data.Mappings;

public class ProfessorMapping : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome);
        builder.Property(x => x.CriadoEm);
        builder.Ignore(x => x.Bloqueado);
        builder.Property(x => x.BloqueadoAte).IsRequired(false);
        builder.HasMany<Aluno>()
            .WithOne(x => x.Professor)
            .HasForeignKey(x => x.ProfessorId);
    }
}