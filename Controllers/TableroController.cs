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
        List<Tablero>tableros=RepoTablero.GetAll();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {   
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero)
    {   
        RepoTablero.Create(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {  
        return View(RepoTablero.GetById(idTablero));
    }


    [HttpPost]
    public IActionResult ModificarTablero(Tablero tablero)
    {   
        RepoTablero.Update(tablero,tablero.Id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTablero(int idTablero)
    {  
        RepoTablero.Remove(idTablero);
        return RedirectToAction("Index");
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
