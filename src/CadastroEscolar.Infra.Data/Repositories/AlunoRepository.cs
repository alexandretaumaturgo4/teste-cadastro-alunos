using CadastroEscolar.Domain.Entities;
using CadastroEscolar.Domain.Repositories;
using CadastroEscolar.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroEscolar.Infra.Data.Repositories;

public class AlunoRepository : IAlunoRepository
{
    private readonly CadastroEscolarContext _context;

    public AlunoRepository(CadastroEscolarContext context)
    {
        _context = context;
    }

    public async Task CriarAluno(Aluno aluno)
    {
        _context.Alunos.Add(aluno);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Aluno>> BuscarAlunos()
    {
        return await _context
            .Alunos
            .Include(x => x.Professor)
            .ToListAsync();
    }

    public async Task<IEnumerable<Aluno>> BuscarPorProfessor(Guid professorId)
    {
        return await _context.Alunos
            .Include(x => x.Professor)
            .Where(x => x.ProfessorId == professorId)
            .ToListAsync();
    }

    public async Task ExcluirAluno(Aluno aluno)
    {
        _context.Alunos.Remove(aluno);

        await _context.SaveChangesAsync();
    }

    public async Task<Aluno> BuscarPorId(Guid alunoId)
    {
        return await _context.Alunos.FirstOrDefaultAsync(x => x.Id == alunoId);
    }

    public async Task AdicionarLote(IEnumerable<Aluno> alunos)
    {
        _context.Alunos.AddRange(alunos);

        await _context.SaveChangesAsync();
    }
}