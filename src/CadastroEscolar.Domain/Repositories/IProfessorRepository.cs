using CadastroEscolar.Domain.Entities;

namespace CadastroEscolar.Domain.Repositories;

public interface IProfessorRepository
{
    Task CriarProfessor(Professor professor);
    Task<IEnumerable<Professor>> ListarProfessores();
    Task<Professor> BuscarProfessorPorId(Guid professorId);
    Task AtualizarProfessor(Professor professor);
}