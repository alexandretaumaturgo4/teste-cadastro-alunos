using CadastroEscolar.Domain.Services.Requests;
using CadastroEscolar.Domain.Services.Responses;
using Microsoft.AspNetCore.Http;

namespace CadastroEscolar.Domain.Services.Interfaces;

public interface IAlunoService
{
    Task CadastrarAluno(AdicionarAlunoRequest request);
    Task<IEnumerable<AlunoResponse>> ListarAlunos();
    Task<IEnumerable<AlunoResponse>> ListarAlunosPorProfessor(Guid professorId);
    Task ExcluirAluno(Guid alunoId);
    Task<IncluirAlunosLoteResponse> IncluirAlunosEmLote(IFormFile file, Guid professorId);
}