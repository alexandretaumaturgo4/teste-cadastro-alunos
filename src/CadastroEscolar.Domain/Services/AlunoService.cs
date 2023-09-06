using CadastroEscolar.Domain.Entities;
using CadastroEscolar.Domain.Repositories;
using CadastroEscolar.Domain.Services.Interfaces;
using CadastroEscolar.Domain.Services.Requests;
using CadastroEscolar.Domain.Services.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CadastroEscolar.Domain.Services;

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IProfessorRepository _professorRepository;
    private readonly IConfiguration _configuration;

    public AlunoService(IAlunoRepository alunoRepository, IProfessorRepository professorRepository,
        IConfiguration configuration)
    {
        _alunoRepository = alunoRepository;
        _professorRepository = professorRepository;
        _configuration = configuration;
    }

    public async Task CadastrarAluno(AdicionarAlunoRequest request)
    {
        Professor professor = await _professorRepository.BuscarProfessorPorId(request.ProfessorId);
        if (professor == null)
        {
            throw new ArgumentException("Professor não encontrado.");
        }

        var aluno = new Aluno(request.Nome, request.Mensalidade, request.DataVencimento, request.ProfessorId);
        await _alunoRepository.CriarAluno(aluno);
    }

    public async Task<IEnumerable<AlunoResponse>> ListarAlunos()
    {
        return (await _alunoRepository.BuscarAlunos()).Select(x => new AlunoResponse
        {
            Mensalidade = x.Mensalidade,
            DataVencimento = x.DataVencimento,
            NomeProfessor = x.Professor.Nome,
            Nome = x.Nome,
            Id = x.Id
        });
    }

    public async Task<IEnumerable<AlunoResponse>> ListarAlunosPorProfessor(Guid professorId)
    {
        return (await _alunoRepository.BuscarPorProfessor(professorId)).Select(x => new AlunoResponse
        {
            Mensalidade = x.Mensalidade,
            DataVencimento = x.DataVencimento,
            NomeProfessor = x.Professor.Nome,
            Nome = x.Nome,
            Id = x.Id
        });
    }

    public async Task ExcluirAluno(Guid alunoId)
    {
        Aluno aluno = await _alunoRepository.BuscarPorId(alunoId);
        if (aluno is null)
        {
            throw new ArgumentNullException("Aluno não encontrado.");
        }

        await _alunoRepository.ExcluirAluno(aluno);
    }

    public async Task<IncluirAlunosLoteResponse> IncluirAlunosEmLote(IFormFile file, Guid professorId)
    {
        try
        {
            var importTimeBlockInSeconds = Convert.ToInt16(_configuration["ImportTimeBlockInSeconds"]);

            var professor = await _professorRepository.BuscarProfessorPorId(professorId);
            if (professor is null)
            {
                return new IncluirAlunosLoteResponse
                {
                    Success = false,
                    Message = "Profesor não encontrado"
                };
            }

            bool isLocked = professor.Bloqueado;
            if (isLocked)
            {
                return new IncluirAlunosLoteResponse
                {
                    Success = false,
                    Message = "Você deverá esperar o tempo de bloqueio para continuar."
                };
            }

            if (file != null && file.Length > 0)
            {
                using var streamReader = new StreamReader(file.OpenReadStream());
                await streamReader.ReadLineAsync();

                string line;

                if ((await streamReader.ReadLineAsync()) is null)
                {
                    return new IncluirAlunosLoteResponse
                    {
                        Success = false,
                        Message = "Arquivo em formato inválido!"
                    };
                }

                var alunos = new List<Aluno>();

                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    var campos = line.Split(new string[] { "||" }, StringSplitOptions.None);

                    if (campos.Length == 3)
                    {
                        string nome = campos[0];
                        decimal mensalidade = decimal.Parse(campos[1]);
                        DateTime dataVencimento = DateTime.Parse(campos[2]);

                        var aluno = new Aluno(nome.Trim(), mensalidade, dataVencimento, professorId);

                        alunos.Add(aluno);
                    }
                    else
                    {
                        return new IncluirAlunosLoteResponse
                        {
                            Success = false,
                            Message = "Arquivo em formato inválido!"
                        };
                    }
                }

                await _alunoRepository.AdicionarLote(alunos);
            }

            professor.Bloquear(importTimeBlockInSeconds);
            await _professorRepository.AtualizarProfessor(professor);
            return new IncluirAlunosLoteResponse
            {
                Success = true,
                Message = "Alunos cadastrados em lote com sucesso!"
            };
        }
        catch (Exception e)
        {
            return new IncluirAlunosLoteResponse
            {
                Success = true,
                Message = "Ocorreu um erro ao cadastrar alunos: " + e.Message
            };
        }
    }
}