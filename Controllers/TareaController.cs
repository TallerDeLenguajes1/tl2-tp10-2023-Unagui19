using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private TareaRepository repoTarea;

    public TareaController(ILogger<TareaController> logger)
    {
        repoTarea = new TareaRepository();
        _logger = logger;
    }

//Listar Tareas
    public IActionResult Index()
    {
        return View(repoTarea.ListarTareas());
    }

//Crear Tareas
    [HttpGet]
    public IActionResult CrearTarea(){
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult CrearTarea(Tarea tarea){
        repoTarea.CrearTarea(tarea);
        return RedirectToAction("Index");
    }

//Modificar tareas
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea){
        return View(repoTarea.BuscarTareaPorId(idTarea));
    }

    [HttpPost]
    public IActionResult ModificarTarea(Tarea tarea){
        repoTarea.ModificarTarea(tarea.Id, tarea);
        return RedirectToAction("Index");
    }

//Eliminar tablero
    public IActionResult EliminarTarea(int idTarea){
        repoTarea.EliminarTarea(idTarea);
        return RedirectToAction("Index");
    }
}