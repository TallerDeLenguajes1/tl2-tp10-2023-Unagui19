using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;
using tl2_tp10_2023_Unagui19.ViewModels;

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
        var VModel = tareas.Select(tarea=> new IndexTareaViewModel(tarea)).ToList();
        return View(VModel);
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {   
        return View(new CrearTareaViewModel());
    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel tareaVM)
    {   
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        var tarea = new Tarea(tareaVM);
        RepoTarea.Create(tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {  
        var VModel = new ModificarTareaViewModel(RepoTarea.GetById(idTarea));
        return View(VModel);

    }


    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel tareaVM)
    {   
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        var tarea = new Tarea(tareaVM);
        RepoTarea.Update(tarea,tarea.Id);
        return RedirectToRoute(new { controller = "Tarea", action = "Index" });
    }

    public IActionResult EliminarTarea(int idTarea)
    {  
        RepoTarea.Remove(idTarea);
        return RedirectToAction("Index");
    }

//Metodos para ver temas de sesion
    // private bool IsAdmin()
    // {
    //     if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Admin")
    //         return true;

    //     return false;
    // }

    // private bool IsUser()
    // {
    //     if (HttpContext.Session != null && (HttpContext.Session.GetString("Rol") == "Admin" || HttpContext.Session.GetString("Rol") == "Operador"))
    //         return true;

    //     return false;
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
