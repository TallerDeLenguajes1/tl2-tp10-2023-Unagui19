using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.Repositorios;

namespace Kanban.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private UsuarioRepository RepoUsuario;
    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        RepoUsuario = new UsuarioRepository();

    }

    public IActionResult Index()
    {
        return View();
    }




    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
