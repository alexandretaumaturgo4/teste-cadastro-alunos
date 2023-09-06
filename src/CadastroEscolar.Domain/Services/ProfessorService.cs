using CadastroEscolar.Domain.Entities;
using CadastroEscolar.Domain.Repositories;
using CadastroEscolar.Domain.Services.Interfaces;
using CadastroEscolar.Domain.Services.Requests;
using CadastroEscolar.Domain.Services.Responses;

namespace CadastroEscolar.Domain.Services;

public class ProfessorService : IProfessorService
{
    private readonly IProfessorRepository _professorRepository;

    public ProfessorService(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository;
    }

    public async Task AdicionarProfessor(AdicionarProfessorRequest request)
    {
        var professor = new Professor(request.Nome);

        await _professorRepository.CriarProfessor(professor);
    }

    public async Task<IEnumerable<ProfessorResponse>> ListarProfessores()
    {
        IEnumerable<Professor> professores =
            await _professorRepository.ListarProfessores();

        var response = professores.Select(x => new ProfessorResponse
        {
            Nome = x.Nome,
            Id = x.Id
        });
        
        return response;
    }
}