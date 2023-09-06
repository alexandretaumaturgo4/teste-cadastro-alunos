using CadastroEscolar.Domain.Services.Requests;
using CadastroEscolar.Domain.Services.Responses;

namespace CadastroEscolar.Domain.Services.Interfaces;

public interface IProfessorService
{
    Task AdicionarProfessor(AdicionarProfessorRequest request);
    Task<IEnumerable<ProfessorResponse>> ListarProfessores();
}