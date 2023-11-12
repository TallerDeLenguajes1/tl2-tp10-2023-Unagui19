using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kanban.Models;
using Kanban.Repositorios;

namespace Kanban.Controllers;

public class  TareaController: Controller
{
    private readonly ILogger<TareaController> _logger;
    private TareaRepository RepoTablero;
    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        RepoTablero = new TareaRepository();

    }

    public IActionResult Index()
    {
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
