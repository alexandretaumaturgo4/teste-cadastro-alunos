using CadastroEscolar.Domain.Services.Interfaces;
using CadastroEscolar.Domain.Services.Requests;
using CadastroEscolar.Domain.Services.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEscolar.Presentation.MVC.Controllers;

public class AlunosController : Controller
{
    private readonly IAlunoService _alunoService;
    private readonly IProfessorService _professorService; 

    public AlunosController(IAlunoService alunoService, IProfessorService professorService)
    {
        _alunoService = alunoService;
        _professorService = professorService; 
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<AlunoResponse> alunos = await _alunoService.ListarAlunos();

        ViewBag.Professores =
            await _professorService.ListarProfessores();

        return View(alunos);
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarAluno(AdicionarAlunoRequest request)
    {
        await _alunoService.CadastrarAluno(request);

        IEnumerable<AlunoResponse> alunos = await _alunoService.ListarAlunos();

        TempData["Mensagem"] = "Aluno adicionado com sucesso!";

        return RedirectToAction("Index", alunos);
    }

    [HttpGet("alunos-professor")]
    public async Task<IActionResult> BuscarAlunosProfessor(Guid professorId, bool mostrarBotaoLote = true)
    {
        var alunos = await _alunoService.ListarAlunosPorProfessor(professorId);

        ViewBag.Professores =
            await _professorService.ListarProfessores();

        ViewBag.MostrarBotaoLote = mostrarBotaoLote;
        ViewBag.ProfessorId = professorId;

        return View("Index", alunos);
    }


    [HttpPost]
    public async Task<IActionResult> ExcluirAluno(Guid alunoId)
    {
        await _alunoService.ExcluirAluno(alunoId);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarAlunosEmLote(IFormFile file, Guid professorId)
    {
        var response = await _alunoService.IncluirAlunosEmLote(file, professorId);

        if (response.Success)
        {
            TempData["Mensagem"] = response.Message;
        }
        else
        {
            TempData["Erro"] = response.Message;
        }

        return RedirectToAction("Index");
    }
}