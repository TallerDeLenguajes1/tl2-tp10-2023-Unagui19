using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.ViewModels;
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

//Listar Usuarios
    public IActionResult Index()
    {
        if(!usuarioLogueado()){
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            List<Tarea> tareas = repoTarea.ListarTareas();
            var VModel = tareas.Select(tar => new IndexTareaViewModel(tar)).ToList();
            return View(VModel);
        }

    }

//Crear Usuario
    [HttpGet]
    public IActionResult CrearTarea(){
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel nuevaTarea){
        var tarea = new Tarea(nuevaTarea);
        repoTarea.CrearTarea(tarea);
        return RedirectToAction("Index");
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea){
        var VModel = new ModificarTareaViewModel(repoTarea.BuscarTareaPorId(idTarea));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel modTarea){
        var tarea = new Tarea(modTarea);
        repoTarea.ModificarTarea(tarea.Id, tarea);
        return RedirectToAction("Index");
    }

//Eliminar tablero
    public IActionResult EliminarTarea(int idTarea){
        repoTarea.EliminarTarea(idTarea);
        return RedirectToAction("Index");
    }

    public bool usuarioLogueado(){
       if (HttpContext.Session.IsAvailable)
       {
            return true;
       } 
       else
       {
            return false;
       }
    }
}