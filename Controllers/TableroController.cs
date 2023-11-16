using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;

namespace tl2_tp10_2023_Unagui19.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private TableroRepository RepoTablero;
    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        RepoTablero = new TableroRepository();

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
