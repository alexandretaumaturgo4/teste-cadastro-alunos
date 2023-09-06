using CadastroEscolar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroEscolar.Infra.Data.Data;

public class CadastroEscolarContext : DbContext
{
    public CadastroEscolarContext(DbContextOptions<CadastroEscolarContext> options) : base(options)
    {
    }

    public DbSet<Professor> Professores { get; set; }
    public DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CadastroEscolarContext).Assembly);
    }
}