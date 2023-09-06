using CadastroEscolar.Domain.Entities;
using CadastroEscolar.Domain.Repositories;
using CadastroEscolar.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace CadastroEscolar.Infra.Data.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    private readonly CadastroEscolarContext _context;

    public ProfessorRepository(CadastroEscolarContext context)
    {
        _context = context;
    }

    public async Task CriarProfessor(Professor professor)
    {
        _context.Professores.Add(professor);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Professor>> ListarProfessores()
    {
        return await _context.Professores.ToListAsync();
    }

    public async Task<Professor> BuscarProfessorPorId(Guid professorId)
    {
        return await _context.Professores.FirstOrDefaultAsync(x => x.Id == professorId);
    }


    public async Task AtualizarProfessor(Professor professor)
    {
        _context.Professores.Update(professor);
        await _context.SaveChangesAsync();
    }
}