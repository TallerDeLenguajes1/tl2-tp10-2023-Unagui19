using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.ViewModels;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _repoTarea;

    public TareaController(ILogger<TareaController> logger, ITareaRepository repoTarea)
    {
        _repoTarea = repoTarea;
        _logger = logger;
    }

//Listar Usuarios
    public IActionResult Index()
    {
            List<Tarea> tareas = _repoTarea.ListarTareas();
            var VModel = tareas.Select(tar => new IndexTareaViewModel(tar)).ToList();
            return View(VModel);
    }

//Crear Usuario
    [HttpGet]
    public IActionResult CrearTarea(){
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel nuevaTarea){
        if (!ModelState.IsValid){return RedirectToAction("Index");}
        var tarea = new Tarea(nuevaTarea);
        _repoTarea.CrearTarea(tarea);
        return RedirectToAction("Index");
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea){
        var VModel = new ModificarTareaViewModel(_repoTarea.BuscarTareaPorId(idTarea));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel modTarea){
        if (!ModelState.IsValid){return RedirectToAction("Index");}
        var tarea = new Tarea(modTarea);
        _repoTarea.ModificarTarea(tarea.Id,tarea);
        return RedirectToAction("Index");
    }

//Eliminar tablero
    public IActionResult EliminarTarea(int idTarea){
        _repoTarea.EliminarTarea(idTarea);
        return RedirectToAction("Index");
    }

    // public bool usuarioLogueado(){
    //    if (HttpContext.Session.IsAvailable)
    //    {
    //         return true;
    //    } 
    //    else
    //    {
    //         return false;
    //    }
    // }
}