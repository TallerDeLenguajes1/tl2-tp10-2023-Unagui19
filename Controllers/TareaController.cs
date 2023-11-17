using System.Diagnostics;
using Kanban.Repositorios;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;

namespace tl2_tp10_2023_Unagui19.Controllers;

public class  TareaController: Controller
{
    private readonly ILogger<TareaController> _logger;
    private TareaRepository RepoTarea;
    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        RepoTarea = new TareaRepository();

    }

    public IActionResult Index()
    {
        List<Tarea>Tareas=RepoTarea.GetAll();
        return View(Tareas);
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {   
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult CrearTarea(Tarea Tarea)
    {   
        RepoTarea.Create(Tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {  
        return View(RepoTarea.GetById(idTarea));
    }


    [HttpPost]
    public IActionResult ModificarTarea(Tarea Tarea)
    {   
        RepoTarea.Update(Tarea,Tarea.Id);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public IActionResult EliminarTarea(Tarea Tarea)
    {  
        RepoTarea.Remove(Tarea.Id);
        return RedirectToAction("Index");
    }



    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
