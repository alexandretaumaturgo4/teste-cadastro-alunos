using CadastroEscolar.Domain.Entities;

namespace CadastroEscolar.Domain.Repositories;

public interface IAlunoRepository
{
    Task CriarAluno(Aluno aluno);
    Task<IEnumerable<Aluno>> BuscarAlunos();
    Task<IEnumerable<Aluno>> BuscarPorProfessor(Guid professorId);
    Task ExcluirAluno(Aluno aluno);
    Task<Aluno> BuscarPorId(Guid alunoId);
    Task AdicionarLote(IEnumerable<Aluno> alunos);
}