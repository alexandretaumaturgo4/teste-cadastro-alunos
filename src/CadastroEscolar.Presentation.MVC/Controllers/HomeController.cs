using System.Diagnostics;
using CadastroEscolar.Domain.Services.Interfaces;
using CadastroEscolar.Domain.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using CadastroEscolar.Presentation.MVC.Models;

namespace CadastroEscolar.Presentation.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IProfessorService _professorService;

    public HomeController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var professores = await _professorService.ListarProfessores();
        return View(professores);
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarProfessor(string nome)
    {
        var professorRequest = new AdicionarProfessorRequest
        {
            Nome = nome
        };

        await _professorService.AdicionarProfessor(professorRequest);

        var professores = await _professorService.ListarProfessores();

        TempData["Mensagem"] = "Professor adicionado com sucesso!";

        return View("Index", professores);
    }

}
