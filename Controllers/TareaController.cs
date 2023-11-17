using System.Diagnostics;
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
        List<Tarea>tareas=RepoTarea.GetAll();
        return View(tareas);
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
    public IActionResult ModificarTarea(Tarea tarea)
    {   
        RepoTarea.Update(tarea,tarea.Id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTarea(int idTarea)
    {  
        RepoTarea.Remove(idTarea);
        return RedirectToAction("Index");
    }



    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
