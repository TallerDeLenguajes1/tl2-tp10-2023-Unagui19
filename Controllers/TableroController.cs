using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private TableroRepository repoTablero;

    public TableroController(ILogger<TableroController> logger)
    {
        repoTablero = new TableroRepository();
        _logger = logger;
    }

//Listar Tableros
    public IActionResult Index()
    {
        return View(repoTablero.ListarTableros());
    }

//Crear Tablero
    [HttpGet]
    public IActionResult CrearTablero(){
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero){
        repoTablero.CrearTablero(tablero);
        return RedirectToAction("Index");
    }

//Modificar tableros
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero){
        return View(repoTablero.BuscarTableroPorId(idTablero));
    }

    [HttpPost]
    public IActionResult ModificarTablero(Tablero tablero){
        repoTablero.ModificarTablero(tablero.Id, tablero);
        return RedirectToAction("Index");
    }

//Eliminar tablero
    public IActionResult EliminarTablero(int idTablero){
        repoTablero.EliminarTablero(idTablero);
        return RedirectToAction("Index");
    }
}